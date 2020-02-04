using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elton.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// 语音结果片段。
    /// </summary>
    public class SpeechPart
    {
        /// <summary>
        /// 当前音频的索引号。
        /// </summary>
        public int TextIndex { get; private set; }
        public int TextLength { get; private set; }
        /// <summary>
        /// 整个转换过程的音频总数。
        /// </summary>
        public int TotalCount { get; private set; }
        public string Text { get; private set; }
        /// <summary>
        /// 音频数据。
        /// </summary>
        public byte[] RawAudio { get; private set; }
        public SpeechPart(int textIndex, int textLength, int totalCount, string text, byte[] rawAudio)
        {
            this.TextIndex = textIndex;
            this.TextLength = textLength;
            this.TotalCount = totalCount;
            this.Text = text;
            this.RawAudio = rawAudio;
        }
    }
}
