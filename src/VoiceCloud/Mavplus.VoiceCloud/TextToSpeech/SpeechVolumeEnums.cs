// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mavplus.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// vol参数的枚举常量，命名不可变更。
    /// </summary>
    public enum SpeechVolumeEnums
    {
        [Description("最轻")]
        x_soft,
        [Description("轻")]
        soft,
        [Description("一般")]
        medium,
        [Description("大")]
        loud,
        [Description("最大")]
        x_loud
    }
    public class SpeechVolumeInfo
    {

        public static string GetKey(SpeechVolumeEnums value)
        {
            string volumeString = "";
            switch (value)
            {
                case SpeechVolumeEnums.x_soft: volumeString = "x-soft"; break;
                case SpeechVolumeEnums.soft: volumeString = "soft"; break;
                case SpeechVolumeEnums.medium: volumeString = "medium"; break;
                case SpeechVolumeEnums.loud: volumeString = "loud"; break;
                case SpeechVolumeEnums.x_loud: volumeString = "x-loud"; break;
            }

            return volumeString;
        }
    }
}
