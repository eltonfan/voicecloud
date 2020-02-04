// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 本地音频文件识别。
    /// </summary>
    public class AudioFileIsrEngine : IsrEngine
    {
        public AudioFileIsrEngine(IsrConfig config, string recog_grammar)
            : base(config, recog_grammar, "sub=iat,ssm=1,auf=audio/L16;rate=16000,aue=speex,ent=sms16k,rst=plain")
        { }

        protected override string RecognizeInternal(BackgroundWorker bw, string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("文件不存在。", fileName);

            string ext = Path.GetExtension(fileName).ToLower();
            if (ext != ".wav" && ext != ".pcm")
                throw new FormatException("音频文件格式不受支持，仅支持wav或pcm。");

            SpeechRecognitionSession session = null;
            FileStream stream = null;
            try
            {
                //打开文件
                stream = new FileStream(fileName, FileMode.Open);
                if (Path.GetExtension(fileName).ToLower() == ".wav")
                    stream.Position = 44;

                session = client.CreateSession();
                byte[] buffer = new byte[session.BlockMaxLength];

                AudioStatus audio_status = AudioStatus.ISR_AUDIO_SAMPLE_INIT;
                // 发送音频数据，获取语音听写结果
                while (audio_status != AudioStatus.ISR_AUDIO_SAMPLE_LAST)
                {
                    if (bw != null && bw.CancellationPending)
                        break;

                    int length = stream.Read(buffer, 0, buffer.Length);
                    audio_status = (length == buffer.Length) ? AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE : AudioStatus.ISR_AUDIO_SAMPLE_LAST;

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
            catch (Exception)
            {
                return session.Result;
            }
            finally
            {
                session.Dispose();
                session = null;
            }
        }

        /// <summary>
        /// 识别本地音频文件。
        /// </summary>
        /// <param name="uriString">音频文件，pcm无文件头，采样率16k，数据16位，单声道</param>
        public override string Recognize(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("文件不存在。", fileName);

            string ext = Path.GetExtension(fileName).ToLower();
            if (ext != ".wav" && ext != ".pcm")
                throw new FormatException("音频文件格式不受支持，仅支持wav或pcm。");

            return base.Recognize(fileName);
        }

        public override void RecognizeAsync(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("文件不存在。", fileName);

            string ext = Path.GetExtension(fileName).ToLower();
            if (ext != ".wav" && ext != ".pcm")
                throw new FormatException("音频文件格式不受支持，仅支持wav或pcm。");

            base.RecognizeAsync(fileName);
        }
    }
}
