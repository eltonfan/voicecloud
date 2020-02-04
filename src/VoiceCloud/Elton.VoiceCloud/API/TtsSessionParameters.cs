// Coded by chuangen http://chuangen.name.

using Elton.VoiceCloud.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud
{
    /// <summary>
    /// 初始化时传入的字符串，以指定识别或听写用到的一些配置参数。
    /// </summary>
    internal class TtsSessionParameters
    {

        public SpeakerEnums Speaker { get; set; }
        public SpeechSpeedEnums Speed { get; set; }
        public SpeechVolumeEnums Volume { get; set; }
        public int SampleRate { get; set; }
        public TtsSessionParameters(SpeakerEnums speaker, SpeechSpeedEnums speed, SpeechVolumeEnums volume, int sampleRate)
        {
            this.Speaker = speaker;
            this.Speed = speed;
            this.Volume = volume;
            this.SampleRate = sampleRate;
        }

        /// <summary>
        /// 生成参数字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string szParams = string.Format("ssm=1,{0},spd={1},aue=speex-wb;7,vol={2},auf=audio/L16;rse=unicode;rate={3}",
                SpeakerInfo.GetExpression(this.Speaker),
                SpeechSpeedInfo.GetKey(this.Speed),
                SpeechVolumeInfo.GetKey(this.Volume),
                this.SampleRate);

            return szParams;
        }
    }
}
