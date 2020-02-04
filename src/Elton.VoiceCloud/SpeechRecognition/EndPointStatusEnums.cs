// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 端点检测（End-point detected）器所处的状态。
    /// </summary>
    public enum EndPointStatusEnums
    {
        ISR_EP_NULL = -1,
        /// <summary>
        /// 还没有检测到音频的前端点
        /// </summary>
        ISR_EP_LOOKING_FOR_SPEECH = 0,
        /// <summary>
        /// 已经检测到了音频前端点，正在进行正常的音频处理。
        /// </summary>
        ISR_EP_IN_SPEECH = 1,
        /// <summary>
        /// 检测到音频的后端点，后继的音频会被MSC忽略。
        /// </summary>
        ISR_EP_AFTER_SPEECH = 3,
        /// <summary>
        /// 超时。
        /// </summary>
        ISR_EP_TIMEOUT = 4,
        /// <summary>
        /// 出现错误。
        /// </summary>
        ISR_EP_ERROR = 5,
        /// <summary>
        /// 音频过大。
        /// </summary>
        ISR_EP_MAX_SPEECH = 6,
    }
}
