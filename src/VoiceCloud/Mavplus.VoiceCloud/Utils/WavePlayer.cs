// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mavplus.VoiceCloud
{
    /// <summary>
    /// 简单的WAV数据播放器。
    /// </summary>
    public class WavePlayer
    {
        readonly ConcurrentQueue<byte[]> queueAudios = new ConcurrentQueue<byte[]>();
        readonly BackgroundWorker bw = null;
        public WavePlayer()
        {
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;

            bw.DoWork += bw_DoWork;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] data = null;
            while (true)
            {
                if (!queueAudios.TryDequeue(out data))
                    break;

                MemoryStream stream = new MemoryStream();

                byte[] headerByte = new WaveHeader(
                    1,//单声道
                    16000,//采样频率
                    16,//每个采样8bit
                    data.Length).ToBytes();
                //写入文件头
                stream.Write(headerByte, 0, headerByte.Length);
                stream.Write(data, 0, data.Length);
                stream.Flush();

                stream.Position = 0;
                System.Media.SoundPlayer pl = new System.Media.SoundPlayer(stream);
                pl.Stop();
                pl.PlaySync();
            }
        }
        public void PlayRawAudioSync(byte[] buffer)
        {
            queueAudios.Enqueue(buffer);

            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }
    }
}
