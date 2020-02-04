// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elton.VoiceCloud.ISR;

namespace Elton.VoiceCloud
{
    public interface IIsrEngine
    {
        /// <summary>
        /// 语音识别。
        /// </summary>
        string Recognize(string uriString);
        void RecognizeAsync(string uriString);
        /// <summary>
        /// 请求取消挂起的后台操作。
        /// </summary>
        void CancelAsync();

        /// <summary>
        /// 是否正在识别中。
        /// </summary>
        bool Recognizing { get; }
        /// <summary>
        /// 调用 ReportProgress 时发生。
        /// </summary>
        event EventHandler<ResultArrivedEventArgs> ResultArrived;
        /// <summary>
        /// 当后台操作已完成、被取消或引发异常时发生。
        /// </summary>
        event EventHandler<RecognizeCompletedEventArgs> RecognizeCompleted;
    }
}
