// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mavplus.VoiceCloud
{
    /// <summary>
    /// WAVE 文件头。
    /// </summary>
    public class WaveHeader
    {
        /// <summary>
        /// WAVE File Header 总长度为44Bytes。
        /// </summary>
        public const int HEADER_LENGTH = 44;

        /// <summary>
        /// 文件长度，除了“RIFF”标识外。= FileLength - 4。
        /// </summary>
        public int ChunkSize { get; private set; }
        /// <summary>
        /// 声道数目，1=单声道，2=双声道。
        /// </summary>
        public short Channels { get; private set; }
        /// <summary>
        /// 采样频率。
        /// </summary>
        public int SampleRate { get; private set; }
        /// <summary>
        /// 数据段的长度。
        /// </summary>
        public int DataLength { get; private set; }
        /// <summary>
        /// 每个采样需要的位数。
        /// </summary>
        public short BitsPerSample { get; private set; }

        public WaveHeader(short channels, int sampleRate, short bitsPerSample, int dataLength)
        {
            this.Channels = channels;
            this.SampleRate = sampleRate;
            this.BitsPerSample = bitsPerSample;
            this.DataLength = dataLength;
        }

        private WaveHeader()
        { }

        public static WaveHeader FromFile(string spath)
        {
            WaveHeader wave = new WaveHeader();
            FileStream fs = new FileStream(spath, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);
            wave.ChunkSize = (int)fs.Length - 8;
            fs.Position = 22;
            wave.Channels = br.ReadInt16();
            fs.Position = 24;
            wave.SampleRate = br.ReadInt32();
            fs.Position = 34;

            wave.BitsPerSample = br.ReadInt16();
            wave.DataLength = (int)fs.Length - 44;
            br.Close();
            fs.Close();

            return wave;
        }
        public byte[] ToBytes()
        {
            byte[] headerData = null;
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                stream.Position = 0;
                bw.Write(new char[4] { 'R', 'I', 'F', 'F' });//ChunkId 4Bytes
                bw.Write((Int32)(this.DataLength + 36));//ChunkSize 4Bytes
                bw.Write(new char[4] { 'W', 'A', 'V', 'E' });//Format 4Bytes
                bw.Write(new char[4] { 'f', 'm', 't', ' ' });//Subchunk1ID 4Bytes
                bw.Write((int)16);//Subchunk1Size 4Bytes, 数值为16或18, 18则最后又附加信息
                bw.Write((Int16)1);//AudioFormat 2Bytes, 编码方式，一般为0x0001
                bw.Write((Int16)this.Channels);//NumChannels 2Bytes
                bw.Write((Int32)this.SampleRate);//SampleRate 4Bytes
                bw.Write((Int32)(SampleRate * ((this.BitsPerSample * this.Channels) / 8)));//ByteRate 4Bytes, 每秒所需字节数,记录每秒的数据量
                bw.Write((Int16)((this.BitsPerSample * this.Channels) / 8));//BlockAlign 2Bytes, 数据块对齐单位(每个采样需要的字节数)
                bw.Write((Int16)this.BitsPerSample);//BitsPerSample 2Bytes
                bw.Write(new char[4] { 'd', 'a', 't', 'a' });//4Bytes subchunk2ID
                bw.Write((Int32)this.DataLength);//4Bytes
                headerData = stream.ToArray();

                bw.Close();
                stream.Close();
            }

            return headerData;
        }
    }
}
