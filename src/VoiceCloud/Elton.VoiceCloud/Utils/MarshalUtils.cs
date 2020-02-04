// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Elton.VoiceCloud
{
    /// <summary>
    /// 非托管内存操作辅助类。
    /// </summary>
    public static class MarshalUtils
    {
        /// <summary>
        /// 指针转字符串
        /// </summary>
        /// <param name="p">指向非托管代码字符串的指针</param>
        /// <returns>返回指针指向的字符串</returns>
        public static string Ptr2Str(IntPtr p)
        {
            List<byte> lb = new List<byte>();
            while (Marshal.ReadByte(p) != 0)
            {
                lb.Add(Marshal.ReadByte(p));
                p = p + 1;
            }
            byte[] bs = lb.ToArray();
            return Encoding.Default.GetString(lb.ToArray());
        }

        /// <summary>
        /// 把结构体转化为字节序列
        /// </summary>
        /// <param name="structure">被转化的结构体</param>
        /// <returns>返回字节序列</returns>
        public static byte[] StructToBytes(Object structure)
        {
            Int32 size = Marshal.SizeOf(structure);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, buffer, false);
                Byte[] bytes = new Byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }
}
