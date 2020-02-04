// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Elton.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// 实现对讯飞语音 TTS 接口封装。
    /// </summary>
    public class TextToSpeechEngine : IDisposable
    {

        /// <summary>
        /// 合成音频的采样频率，8000、16000、44100等
        /// </summary>
        public int SampleRate { get; set; }

        public int BitsPerSample { get; private set; }

        public int Channels { get; private set; }

        /// <summary>
        /// 语速。
        /// </summary>
        public SpeechSpeedEnums Speed { get; set; }

        /// <summary>
        /// 音量。
        /// </summary>
        public SpeechVolumeEnums Volume { get; set; }

        /// <summary>
        /// 朗读者。
        /// </summary>
        public SpeakerEnums Speaker { get; set; }

        /// <summary>
        /// 当前文本片段中已完成的部分。
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        readonly byte[] dataDing = Properties.Resources.PCM_DING;
        /// <summary>
        /// 构造函数，初始化引擎。
        /// </summary>
        /// <param name="config">配置。</param>
        public TextToSpeechEngine(TtsConfig config)
        {
            this.Speed = SpeechSpeedEnums.medium;
            this.Volume = SpeechVolumeEnums.x_loud;
            this.BitsPerSample = 16;
            this.SampleRate = 16000;
            this.Channels = 1;

            TTSInterop.Init(config);
        }

        public void Dispose()
        {
            TTSInterop.End();
        }

        protected void RawAudioFromDing(List<SpeechPart> output)
        {
            output.Add(
                new SpeechPart(0, 4, 4, "ting",
                dataDing));
        }

        /// <summary>
        /// 暂停一段时间
        /// </summary>
        /// <param name="output"></param>
        /// <param name="seconds"></param>
        protected void RawAudioFromSleep(List<SpeechPart> output, int seconds)
        {
            int dataLengthPerSecond = SampleRate * this.BitsPerSample * this.Channels / 8;
            output.Add(new SpeechPart(0, seconds, seconds, "", new byte[dataLengthPerSecond * seconds]));
        }


        string SessionId = null;
        /// <summary>
        /// 把文本转换成声音，写入指定的内存流
        /// </summary>
        /// <param name="text">要转化成语音的文字</param>
        /// <param name="stream">合成结果输出的音频流</param>
        protected void RawAudioFromText(List<SpeechPart> output, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
            try
            {
                if (this.SessionId == null)
                {
                    this.SessionId = TTSInterop.SessionBegin(
                        new TtsSessionParameters(this.Speaker, this.Speed, this.Volume, this.SampleRate));
                }
                TTSInterop.TextPut(this.SessionId, text, null);
                SynthStatusEnums synth_status = SynthStatusEnums.TTS_FLAG_STILL_HAVE_DATA;

                int lastTextOffset = 0;//上次结果对应文本偏移量
                int lastTextLength = 0;//上次结果对应文本长度
                byte[] tmpArray = Encoding.Unicode.GetBytes(text);
                while (synth_status == SynthStatusEnums.TTS_FLAG_STILL_HAVE_DATA)
                {
                    byte[] audioData = TTSInterop.AudioGet(this.SessionId, ref synth_status);
                    string posStr = TTSInterop.AudioInfo(this.SessionId);
                    int currentPosition = int.Parse(posStr.Substring(4));
                    if (currentPosition - (lastTextOffset + lastTextLength) <= 0)
                    {//还是上次那段文本的结果
                        //保持不变
                    }
                    else
                    {//新段文本的结果
                        lastTextOffset = lastTextOffset + lastTextLength;
                        lastTextLength = currentPosition - lastTextOffset;
                    }
                    SpeechPart speechPart = new SpeechPart(
                        lastTextOffset / 2,
                        lastTextLength / 2,
                        tmpArray.Length / 2,
                        Encoding.Unicode.GetString(tmpArray, lastTextOffset, lastTextLength),
                        audioData);
                    if (ProgressChanged != null)
                        ProgressChanged(this, new ProgressChangedEventArgs(speechPart));

                    output.Add(speechPart);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            finally
            {
                TTSInterop.SessionEnd(this.SessionId, "normal end");
                this.SessionId = null;
            }
        }

        public void SaveAsWaveFile(string fileName, IEnumerable<SpeechPart> audioData)
        {
            int dataLength = 0;
            foreach(SpeechPart item in audioData)
                dataLength += item.RawAudio.Length;

            byte[] headerByte = new WaveHeader(
                1,//单声道
                16000,//采样频率
                16,//每个采样8bit
                dataLength).ToBytes();

            using(FileStream stream = File.Create(fileName))
            {
                //写入文件头
                stream.Write(headerByte, 0, headerByte.Length);
                foreach (SpeechPart item in audioData)
                    stream.Write(item.RawAudio, 0, item.RawAudio.Length);

                stream.Flush();
                stream.Close();
            }
        }

        /// <summary>
        /// 把文字转化为声音,单路配置，一种语音
        /// </summary>
        /// <param name="text">要转化成语音的文字</param>
        /// <param name="outWaveFlie">把声音转为文件，默认为不生产wave文件</param>
        public void ToWaveFile(string fileName, string content)
        {
            List<SpeechPart> rawAudio = new List<SpeechPart>();
            RawAudioFromText(rawAudio, content);
            SaveAsWaveFile(fileName, rawAudio);
        }


        BackgroundWorker bwSpeakText = null;
        public void SpeakText(string content)
        {
            if(bwSpeakText == null)
            {
                bwSpeakText = new BackgroundWorker();
                bwSpeakText.DoWork += bwSpeakText_DoWork;
                bwSpeakText.WorkerSupportsCancellation = true;
                bwSpeakText.WorkerReportsProgress = true;
                bwSpeakText.ProgressChanged += bwSpeakText_ProgressChanged;
            }
            if (!bwSpeakText.IsBusy)
                bwSpeakText.RunWorkerAsync(content);
        }
        void bwSpeakText_DoWork(object sender, DoWorkEventArgs e)
        {
            string content = e.Argument as string;
            List<SpeechPart> rawAudio = new List<SpeechPart>();
            RawAudioFromText(rawAudio, content);
        }

        void bwSpeakText_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        }

        /// <summary>
        /// 已合成文本片段。
        /// </summary>
        public event EventHandler<AudioReceivedEventArgs> AudioReceived;

        /// <summary>
        /// 把文字转化为声音,多路配置，多种语音
        /// </summary>
        /// <param name="speekText">要转化成语音的文字</param>
        /// <param name="outWaveFlie">把声音转为文件，默认为不生产wave文件</param>
        protected void RawAudioFromFormatText(List<SpeechPart> output, string text)
        {
            try
            {
                string[] lines = text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    int index = output.Count;
                    try
                    {
                        if (lines[i] == null)
                            continue;
                        string line = lines[i].Trim();
                        if (line == "")
                            continue;

                        int pos = line.IndexOf('#');//#在段中的位置
                        if (pos > 0)
                        {
                            //设定了讲话者时用指定的讲话者说
                            this.Speaker = SpeakerInfo.Parse(line.Substring(0, pos).ToLower());
                            string content = line.Substring(pos + 1, line.Length - pos - 1);

                            RawAudioFromText(output, content);
                        }
                        else if (line.StartsWith("stop"))
                        {//暂停一段时间
                            int seconds = Convert.ToInt32(line.Substring(4, line.Length - 4));

                            RawAudioFromSleep(output, seconds);
                        }
                        else if (line.StartsWith("ting"))
                        {//插入叮铃声
                            RawAudioFromDing(output);
                        }
                        else
                        {
                            RawAudioFromText(output, line);
                        }
                    }
                    finally
                    {
                        if (this.AudioReceived != null)
                        {
                            for (int blockId = index; blockId < output.Count; blockId++)
                            {
                                AudioReceivedEventArgs e = new AudioReceivedEventArgs(
                                    index, lines.Length, output[blockId].RawAudio);
                                this.AudioReceived(this, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public void SpeakFormatText(string content)
        {
            List<SpeechPart> rawAudio = new List<SpeechPart>();
            RawAudioFromFormatText(rawAudio, content);

            //foreach (byte[] item in rawAudio)
            //    player.PlayRawAudioSync(item);
        }
    }
}
