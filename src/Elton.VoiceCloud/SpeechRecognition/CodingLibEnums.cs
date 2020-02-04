// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 音频压缩所用编解码库。
    /// </summary>
    [Flags]
    public enum CodingLibEnums : int
    {
        /// <summary>
        /// 空。
        /// </summary>
        None = 0,
        [Description("amr.dll")]
        AmrDll,
        [Description("amr_fx.dll")]
        AmrFxDll,
        [Description("amr_wb.dll")]
        AmrWbDll,
        [Description("amr_wb_fx.dll")]
        AmrWbFxDll,
        [Description("speex.dll")]
        SpeexDll,
    }
}
