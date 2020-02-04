// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// speed语速参数的枚举常量，命名不可变更。
    /// </summary>
    public enum SpeechSpeedEnums
    {
        [Description("最慢")]
        x_slow,
        [Description("慢")]
        slow,
        [Description("一般")]
        medium,
        [Description("快")]
        fast,
        [Description("最快")]
        x_fast,
    }

    public class SpeechSpeedInfo
    {

        public static string GetKey(SpeechSpeedEnums value)
        {
            string speedString = null;
            switch (value)
            {
                case SpeechSpeedEnums.x_slow: speedString = "x-slow"; break;
                case SpeechSpeedEnums.slow: speedString = "slow"; break;
                case SpeechSpeedEnums.medium: speedString = "medium"; break;
                case SpeechSpeedEnums.fast: speedString = "fast"; break;
                case SpeechSpeedEnums.x_fast: speedString = "x-fast"; break;
            }

            return speedString;
        }
    }

}
