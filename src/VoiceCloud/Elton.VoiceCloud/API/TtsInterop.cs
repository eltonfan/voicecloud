// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Elton.VoiceCloud.TextToSpeech
{
    /// <summary>
    ///MSP2.0客户端子系统中关于TTS的函数。
    /// </summary>
    internal class TTSInterop
    {
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern MspErrors QTTSInit(string configs);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr QTTSSessionBegin(string _params, ref MspErrors errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern MspErrors QTTSTextPut(string sessionID, string textString, uint textLen, string _params);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr QTTSAudioGet(string sessionID, ref int audioLen, ref SynthStatusEnums synthStatus, ref MspErrors errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr QTTSAudioInfo(string sessionID);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern MspErrors QTTSSessionEnd(string sessionID, string hints);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern MspErrors QTTSGetParam(string sessionID, string paramName, string paramValue, ref uint valueLen);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern MspErrors QTTSFini();

        /// <summary>
        /// QISRInit应当在应用程序初始化时仅调用一次。
        /// </summary>
        /// <param name="config"></param>
        public static void Init(TtsConfig config)
        {
            MspErrors result = QTTSInit(config.ToString());
            switch (result)
            {
                case MspErrors.MSP_SUCCESS://成功
                    break;
                case MspErrors.MSP_ERROR_CONFIG_INITIALIZE:
                    throw new Exception("初始化TTS的config实例时出现错误。");
                default:
                    throw new Exception("QTTS初始化失败，错误代码: " + result);
            }
        }

        public static string SessionBegin(TtsSessionParameters parameters)
        {
            MspErrors result = MspErrors.MSP_SUCCESS;
            string sessionId = MarshalUtils.Ptr2Str(QTTSSessionBegin(parameters.ToString(), ref result));
            if (result != MspErrors.MSP_SUCCESS)
                throw new Exception("初始化TTS会话时发生错误，" + result);

            return sessionId;
        }
        public static void TextPut(string sessionID, string text, TtsSessionParameters parameters = null)
        {
            int length = Encoding.Unicode.GetByteCount(text);
            MspErrors result = QTTSTextPut(sessionID, text, (uint)length,
                (parameters == null) ? null : parameters.ToString());
            if (result != MspErrors.MSP_SUCCESS)
                throw new Exception("向服务器发送数据，" + result);
        }
        public static byte[] AudioGet(string sessionID, ref SynthStatusEnums synthStatus)
        {
            int audioLength = 0;

            MspErrors result = MspErrors.MSP_SUCCESS;
            IntPtr pAudioData = QTTSAudioGet(sessionID, ref audioLength, ref synthStatus, ref result);
            if (result != MspErrors.MSP_SUCCESS)
                throw new Exception("获取语音结果出错，" + result);
            if (audioLength < 0)
                return null;
            byte[] audioData = new byte[audioLength];
            if (audioLength > 0)
                Marshal.Copy(pAudioData, audioData, 0, audioLength);

            return audioData;
        }

        /// <summary>
        /// ced=XXX
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static string AudioInfo(string sessionId)
        {
            return MarshalUtils.Ptr2Str(TTSInterop.QTTSAudioInfo(sessionId));
        }

        public static void SessionEnd(string sessionId, string reason)
        {
            MspErrors result = QTTSSessionEnd(sessionId, reason);
            if (result != MspErrors.MSP_SUCCESS)
                throw new Exception("结束TTS会话错误，错误代码：" + result);
        }

        public static void End()
        {
            MspErrors result = QTTSFini();
            if (result != MspErrors.MSP_SUCCESS)
                throw new Exception("QTTSFini failed, error code is " + result);
        }
    }
}
