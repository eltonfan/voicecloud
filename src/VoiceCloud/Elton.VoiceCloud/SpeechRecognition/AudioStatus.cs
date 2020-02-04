// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 用来指明用户本次识别的音频是否发送完毕。
    /// </summary>
    /// <remarks>在MSP20中，ISR_AUDIO_SAMPLE_LAST（0x04）用来指明当前的音频已经发送完毕，
    /// 除此之外的任何值都将被MSC视为还有后继的音频。</remarks>
    public enum AudioStatus
    {
        ISR_AUDIO_SAMPLE_INIT = 0x00,
        /// <summary>
        /// 第一块音频
        /// </summary>
        ISR_AUDIO_SAMPLE_FIRST = 0x01,
        /// <summary>
        /// 还有后继音频
        /// </summary>
        ISR_AUDIO_SAMPLE_CONTINUE = 0x02,
        /// <summary>
        /// 最后一块音频
        /// </summary>
        ISR_AUDIO_SAMPLE_LAST = 0x04,
        ISR_AUDIO_SAMPLE_SUPPRESSED = 0x08,
        ISR_AUDIO_SAMPLE_LOST = 0x10,
        ISR_AUDIO_SAMPLE_NEW_CHUNK = 0x20,
        ISR_AUDIO_SAMPLE_END_CHUNK = 0x40,

        ISR_AUDIO_SAMPLE_VALIDBITS = 0x7f /* to validate the value of sample->status */
    }
}
