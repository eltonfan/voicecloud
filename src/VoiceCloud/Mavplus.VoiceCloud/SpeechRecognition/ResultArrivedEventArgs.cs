// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mavplus.VoiceCloud.ISR
{
    /// <summary>
    /// 有识别结果到达。
    /// </summary>
    public class ResultArrivedEventArgs : EventArgs
    {
        /// <summary>
        /// 识别结果字符串。
        /// </summary>
        public string Text { get; private set; }
        public ResultArrivedEventArgs(string text)
        {
            this.Text = text;
        }
    }
}
