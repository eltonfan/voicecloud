// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Elton.VoiceCloud.ISR
{
   public class SpeechRecognitionSession : IDisposable
   {
       const int BLOCK_LEN = 5 * 1024;
       const int waitTime = 5000;

       readonly string recog_grammar = "builtin:grammar/../search/location.abnf?language=zh-cn";
       readonly string recog_params = null;
       readonly IntPtr ptrBuffer = IntPtr.Zero;
       string sessionId = null;
       RecogStatusEnums rslt_status = RecogStatusEnums.ISR_REC_NULL;
       /// <summary>
       /// 识别的结果。
       /// </summary>
       readonly StringBuilder result = new StringBuilder();
       public SpeechRecognitionSession(string recog_grammar, string recog_params)
       {
           this.recog_grammar = recog_grammar;
           this.recog_params = recog_params;

           this.ptrBuffer = Marshal.AllocHGlobal(BLOCK_LEN);
       }

       /// <summary>
       /// 会话一次能够发送的最大数据长度。
       /// </summary>
       public int BlockMaxLength
       {
           get { return BLOCK_LEN; }
       }

       public void Open()
       {
           //开始一路会话，使用会话模式，使用引擎内置的语法进行识别
           this.sessionId = IsrInterop.SessionBegin(this.recog_grammar, this.recog_params);
       }

       public void Dispose()
       {
           //结束会话，释放资源
           int ret = IsrInterop.QISRSessionEnd(sessionId, string.Empty);

           if (ptrBuffer != IntPtr.Zero)
               Marshal.FreeHGlobal(ptrBuffer);
       }

       /// <summary>
       /// 添加音频文件。
       /// </summary>
       /// <param name="buffer"></param>
       /// <param name="offset"></param>
       /// <param name="length"></param>
       /// <param name="audio_status"></param>
       /// <param name="ep_status">端点检测（End-point detected）器所处的状态</param>
       public string AppendAudio(byte[] buffer, int offset, int length, AudioStatus audio_status, out EndPointStatusEnums ep_status)
       {
           if (length > BLOCK_LEN)
               throw new ArgumentException("length超过了最大发送长度。", "length");

           //返回部分结果
           string partResult = null;

           RecogStatusEnums rec_status;//识别器所处的状态
           Marshal.Copy(buffer, 0, ptrBuffer, length);

           ///开始向服务器发送音频数据
           IsrInterop.AudioWrite(sessionId, ptrBuffer, (uint)length, audio_status, out ep_status, out rec_status);
           ///服务器返回部分结果
           if (rec_status == RecogStatusEnums.ISR_REC_STATUS_SUCCESS)
           {//已经有结果缓存在MSC中了，可以获取了
               partResult = IsrInterop.GetResult(sessionId, out this.rslt_status, waitTime);
               if (!string.IsNullOrEmpty(partResult))
                   this.result.Append(partResult);
           }

           return partResult;
       }

       /// <summary>
       /// 结束会话。
       /// </summary>
       public string Finish()
       {
           StringBuilder lastResult = new StringBuilder();

           int loop_count = 0;
           //获取余下的识别结果
           EndPointStatusEnums ep_status = EndPointStatusEnums.ISR_EP_NULL;
           RecogStatusEnums rec_status = RecogStatusEnums.ISR_REC_NULL;
           ///最后一块数据发完之后，循环从服务器端获取结果
           IsrInterop.AudioWrite(this.sessionId, IntPtr.Zero, 0, AudioStatus.ISR_AUDIO_SAMPLE_LAST, out ep_status, out rec_status);
           ///考虑到网络环境不好的情况下，需要对循环次数作限定
           while (this.rslt_status != RecogStatusEnums.ISR_REC_STATUS_SPEECH_COMPLETE && loop_count++ < 30)
           {
               string str = null;
               try
               {
                   str = IsrInterop.GetResult(sessionId, out rslt_status, waitTime);
               }
               catch (EngineNoDataException)
               {
                   break;
               }
               if (!string.IsNullOrEmpty(str))
               {
                   this.result.Append(str);
                   lastResult.Append(str);
               }
               System.Threading.Thread.Sleep(200);
           }

           return lastResult.ToString();
       }

       public string UploadData(string keywords)
       {
           return IsrInterop.UploadData(this.sessionId, keywords);
       }

       public string Result
       {
           get { return result.ToString(); }
       }
   }
}
