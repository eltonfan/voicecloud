// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 识别网络串流的语音。
    /// </summary>
    public class NetworkAudioIsrEngine : IsrEngine
    {
        public NetworkAudioIsrEngine(IsrConfig config, string recog_grammar, string recog_params = null)
            : base(config, recog_grammar, recog_params ?? "sub=iat,ssm=1,auf=audio/L16;rate=16000,aue=speex,ent=sms16k,rst=plain")
        { }

        protected override string RecognizeInternal(BackgroundWorker bw, string httpAudioUrl)
        {
            SpeechRecognitionSession session = null;
            HttpWebRequest webRequest = null;
            HttpWebResponse resp = null;
            Stream stream = null;
            try
            {
                //连接音频源
                webRequest = WebRequest.Create(httpAudioUrl) as HttpWebRequest;
                resp = (HttpWebResponse)webRequest.GetResponse();
                stream = resp.GetResponseStream();

                session = client.CreateSession();
                byte[] buffer = new byte[session.BlockMaxLength];

                AudioStatus audio_status = AudioStatus.ISR_AUDIO_SAMPLE_INIT;
                // 发送音频数据，获取语音听写结果
                while (audio_status != AudioStatus.ISR_AUDIO_SAMPLE_LAST)
                {
                    if (bw != null && bw.CancellationPending)
                        break;

                    int length = stream.Read(buffer, 0, buffer.Length);
                    //audio_status = (length == buffer.Length) ? AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE : AudioStatus.ISR_AUDIO_SAMPLE_LAST;
                    audio_status = AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE;

                    //端点检测器所处的状态
                    EndPointStatusEnums ep_status;
                    string partResult = session.AppendAudio(buffer, 0, length, audio_status, out ep_status);
                    if (!string.IsNullOrEmpty(partResult) && bw != null)
                        bw.ReportProgress(0, partResult);

                    if (ep_status == EndPointStatusEnums.ISR_EP_AFTER_SPEECH)
                    {//检测到音频后端点，结束音频发送
                        break;
                    }

                    //如果是实时采集音频，可以省略此操作。5KB大小的16KPCM持续的时间是160毫秒
                    System.Threading.Thread.Sleep(160);
                }

                string lastResult = session.Finish();
                if (!string.IsNullOrEmpty(lastResult) && bw != null)
                    bw.ReportProgress(0, lastResult);
                return session.Result;
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.RequestCanceled)
                {//不是视频流结束
                    throw ex;
                }
                //正常结束
                return session.Result;
            }
            catch (Exception ex)
            {
                return session.Result;
            }
            finally
            {
                session.Dispose();
                session = null;

                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
                if (webRequest != null)
                    webRequest = null;
            }
        }

        public override string Recognize(string httpAudioUrl)
        {
            return base.Recognize(httpAudioUrl);
        }

        public override void RecognizeAsync(string httpAudioUrl)
        {
            base.RecognizeAsync(httpAudioUrl);
        }
    }
}
