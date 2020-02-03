using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mavplus.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// 收到部分文本合成的结果。
    /// </summary>
    public class AudioReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 当前音频的索引号。
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// 整个转换过程的音频总数。
        /// </summary>
        public int TotalCount { get; private set; }

        public byte[] Data { get; private set; }
        public AudioReceivedEventArgs(int index, int totalCount, byte[] data)
        {
            this.Index = index;
            this.TotalCount = totalCount;
            this.Data = data;
        }
    }
}
