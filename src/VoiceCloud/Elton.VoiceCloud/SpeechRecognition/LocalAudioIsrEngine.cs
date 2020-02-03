// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Mavplus.VoiceCloud.ISR
{
    /// <summary>
    /// 识别本地麦克风的语音。
    /// </summary>
    public class LocalAudioIsrEngine : IsrEngine, IDisposable
    {
        readonly IWaveIn wis = null;
        readonly IWin32Window owner = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner">音频设备必须在主线程初始化，需要UI控件引用。</param>
        /// <param name="config"></param>
        /// <param name="recog_grammar"></param>
        /// <param name="recog_params"></param>
        public LocalAudioIsrEngine(IWin32Window owner, IsrConfig config, string recog_grammar, string recog_params = null)
            : base(config, recog_grammar, recog_params ?? "sub=iat,ssm=1,auf=audio/L16;rate=16000,aue=speex,ent=sms16k,rst=plain")
        {
            this.owner = owner;
            wis = new WaveIn(owner.Handle);
            wis.WaveFormat = new WaveFormat(16000, 16, 1);
        }

        public void Dispose()
        {
            wis.Dispose();
        }

        protected override string RecognizeInternal(BackgroundWorker bw, string uriString)
        {
            bool finished = false;
            SpeechRecognitionSession session = null;
            EventHandler<WaveInEventArgs> handler = delegate(object sender, WaveInEventArgs e)
            {
                try
                {
                    AudioStatus status = AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE;
                    EndPointStatusEnums ep_status = EndPointStatusEnums.ISR_EP_NULL;

                    ///开始向服务器发送音频数据
                    string partResult = session.AppendAudio(e.Buffer, 0, e.BytesRecorded, status, out ep_status);
                    if (!string.IsNullOrEmpty(partResult))
                    {
                        this.OnResultArrived(new ResultArrivedEventArgs(partResult));
                    }

                    if (ep_status == EndPointStatusEnums.ISR_EP_AFTER_SPEECH)
                    {//检测到音频后端点，结束音频发送
                        finished = true;
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            };

            try
            {
                session = client.CreateSession();

                wis.DataAvailable += handler;
                wis.StartRecording();

                while (true)
                {
                    if (bw != null && bw.CancellationPending)
                        break;
                    if (finished)
                        break;

                    Thread.Sleep(200);
                }

                wis.DataAvailable -= handler;
                wis.StopRecording();

                string lastResult = session.Finish();
                if (!string.IsNullOrEmpty(lastResult))
                    this.OnResultArrived(new ResultArrivedEventArgs(lastResult));

                string result = session.Result;
                return result;
            }
            catch (Exception ex)
            {
                return session.Result;
            }
            finally
            {
                if (session != null)
                {
                    session.Dispose();
                    session = null;
                }
            }
        }
    }
}