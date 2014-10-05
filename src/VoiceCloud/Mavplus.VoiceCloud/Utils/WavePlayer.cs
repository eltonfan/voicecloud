// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Mavplus.VoiceCloud.TextToSpeech;

namespace Mavplus.VoiceCloud
{
    /// <summary>
    /// 简单的WAV数据播放器。
    /// </summary>
    public class WavePlayer
    {
        readonly ConcurrentQueue<SpeechPart> queueAudios = new ConcurrentQueue<SpeechPart>();
        readonly BackgroundWorker bw = null;
        public WavePlayer()
        {
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
        }

        void bw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
                this.ProgressChanged(this, new TextToSpeech.ProgressChangedEventArgs(e.UserState as SpeechPart));
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            SpeechPart data = null;
            while (true)
            {
                if (!queueAudios.TryDequeue(out data))
                {
                    Thread.Sleep(100);
                    continue;
                }

                bw.ReportProgress(
                    (int)(100.0 * (data.TextIndex + data.TextLength) / data.TotalCount),
                    data);

                MemoryStream stream = new MemoryStream();

                byte[] headerByte = new WaveHeader(
                    1,//单声道
                    16000,//采样频率
                    16,//每个采样8bit
                    data.RawAudio.Length).ToBytes();
                //写入文件头
                stream.Write(headerByte, 0, headerByte.Length);
                stream.Write(data.RawAudio, 0, data.RawAudio.Length);
                stream.Flush();

                stream.Position = 0;
                System.Media.SoundPlayer pl = new System.Media.SoundPlayer(stream);
                pl.Stop();
                pl.PlaySync();
            }
        }
        public void PlayRawAudioSync(SpeechPart buffer)
        {
            queueAudios.Enqueue(buffer);

            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }

        public event EventHandler<TextToSpeech.ProgressChangedEventArgs> ProgressChanged;
    }
}
