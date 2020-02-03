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
using NAudio.Wave;

namespace Mavplus.VoiceCloud
{
    /// <summary>
    /// 简单的WAV数据播放器。
    /// </summary>
    public class WavePlayer
    {
        readonly ConcurrentQueue<SpeechPart> queueAudios = new ConcurrentQueue<SpeechPart>();
        readonly BackgroundWorker bw = null;
        readonly BufferedWaveProvider PlayBuffer = null;
        readonly WaveOut waveOut = null;
        public WavePlayer()
        {
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;

            PlayBuffer = new BufferedWaveProvider(new WaveFormat(16000, 16, 1));
            waveOut = new WaveOut();
            waveOut.Init(PlayBuffer);
            waveOut.Play();
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

                byte[] rawAudio = data.RawAudio;
                int position = 0;
                while (true)
                {
                    if (PlayBuffer.BufferedBytes > 16000 * 16 / 8 / 5)
                    {//0.5ms
                        Thread.Sleep(100);
                        continue;
                    }
                    if (position >= rawAudio.Length)
                        break;

                    int blockSize = Math.Min(16000 * 16 / 8, rawAudio.Length - position);
                    PlayBuffer.AddSamples(rawAudio, position, blockSize);
                    position += blockSize;
                }
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
