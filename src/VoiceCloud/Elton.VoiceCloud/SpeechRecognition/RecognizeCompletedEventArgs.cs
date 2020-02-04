// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 为 RecognizeCompleted 事件提供数据。
    /// </summary>
    public class RecognizeCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// 识别结果字符串。
        /// </summary>
        public string Result { get; private set; }
        /// <summary>
        /// 初始化 <see cref="RecognizeCompletedEventArgs" /> 类的新实例。
        /// </summary>
        /// <param name="result">异步操作的结果。</param>
        /// <param name="error">在异步操作期间发生的任何错误。</param>
        /// <param name="cancelled">一个指示异步操作是否已被取消的值。<c>true</c> [cancelled].</param>
        public RecognizeCompletedEventArgs(string result, Exception error, bool cancelled)
            : base(error, cancelled, null)
        {
            this.Result = result;
        }
    }
}
