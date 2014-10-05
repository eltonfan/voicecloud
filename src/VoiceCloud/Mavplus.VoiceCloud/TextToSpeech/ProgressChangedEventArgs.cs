// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mavplus.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// 收到部分文本合成的结果。
    /// </summary>
    public class ProgressChangedEventArgs : EventArgs
    {
        public SpeechPart Data { get; private set; }
        public ProgressChangedEventArgs(SpeechPart data)
        {
            this.Data = data;
        }
    }
}
