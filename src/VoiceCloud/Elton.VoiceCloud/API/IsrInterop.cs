// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Mavplus.VoiceCloud.ISR
{
    /// <summary>
    /// MSP2.0客户端子系统中关于ISR的函数。
    /// </summary>
    internal class IsrInterop
    {
        /// <summary>
        /// 对MSC在识别过程中用到的全局配置项参数进行初始化，如服务器地址、访问超时设置等。
        /// </summary>
        /// <param name="configs">初始化时传入的字符串，以指定识别或听写用到的一些配置参数，各个参数以“参数名=参数值”的形式出现，大小写不敏感，不同的参数之间以“,”或“\n”隔开。</param>
        /// <returns>如果函数调用成功返回MSP_SUCCESS，否则返回错误代码，错误代码参见msp_errors.h。</returns>
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern MspErrors QISRInit(string configs);

        /// <summary>
        /// 本接口用来开始一路ISR会话，并在参数中指定本路ISR会话用到的语法列表，本次会话所用的参数等。
        /// </summary>
        /// <param name="grammarList">uri-list格式的语法，可以是一个语法文件的URL或者一个引擎内置语法列表。可以同
        /// 时指定多个语法，不同的语法之间以“,”隔开。进行语音听写时不需要语法，此参数设定为NULL或空串即可；
        /// 进行语音识别时则需要语法，语法可以在此参数中指定，也可以随后调用QISRGrammarActivate指定识别所用的语法。</param>
        /// <param name="_params">本路ISR会话使用的参数，可设置的参数及其取值范围请参考《可设置参数列表_MSP20.xls》，各个参数以“参数名=参数值”的形式出现，不同的参数之间以“,”或者“\n”隔开。</param>
        /// <param name="errorCode">如果函数调用成功则其值为MSP_SUCCESS，否则返回错误代码，错误代码参见msp_errors.h。</param>
        /// <returns>MSC为本路会话建立的ID，用来唯一的标识本路会话，供以后调用其他函数时使用。函数调用失败则会返回NULL。</returns>
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr QISRSessionBegin(string grammarList, string _params, out MspErrors errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRGrammarActivate(string sessionID, string grammar, string type, int weight);

        /// <summary>
        /// 写入本次识别的音频，音频可以一次性写入，也可以多次调用此接口分批写入。
        /// </summary>
        /// <param name="sessionID">由QISRSessionBegin返回过来的会话ID。</param>
        /// <param name="waveData">音频数据缓冲区起始地址。</param>
        /// <param name="waveLen">音频数据长度，其大小不能超过设定的max_audio_size。</param>
        /// <param name="audioStatus">用来指明用户本次识别的音频是否发送完毕，</param>
        /// <param name="epStatus">端点检测（End-point detected）器所处的状态。
        /// 当epStatus大于等于3时，用户应当停止写入音频的操作，否则写入MSC的音频会被忽略。</param>
        /// <param name="recogStatus">识别器所处的状态。</param>
        /// <returns></returns>
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern MspErrors QISRAudioWrite(string sessionID, IntPtr waveData, uint waveLen, AudioStatus audioStatus, out EndPointStatusEnums epStatus, out RecogStatusEnums recogStatus);

        /// <summary>
        /// 获取识别结果。
        /// </summary>
        /// <param name="sessionID">由QISRSessionBegin返回过来的会话ID。</param>
        /// <param name="rsltStatus">识别结果的状态，其取值范围和含义请参考QISRAudioWrite的参数recogStatus。</param>
        /// <param name="waitTime">与服务器交互的间隔时间，可以控制和服务器的交互频度。单位为ms，建议取值为5000。</param>
        /// <param name="errorCode">如果函数调用成功返回MSP_SUCCESS，否则返回错误代码，错误代码参见msp_errors.h。</param>
        /// <returns></returns>
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr QISRGetResult(string sessionID, out RecogStatusEnums rsltStatus, int waitTime, out MspErrors errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRSessionEnd(string sessionID, string hints);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRGetParam(string sessionID, string paramName, string paramValue, ref uint valueLen);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern MspErrors QISRFini();



        /** 
         * @fn		QISRUploadData
         * @brief	Upload User Specific Data
         * 
         *  Upload data such as user config,custom grammar,etc.
         * 
         * @return	const char* MSPAPI		- extern info return by Server, used for special command.
         * @param	const char* sessionID	- [in] session id returned by session begin,if without session, set NULL
         * @param	const char* dataName	- [in] data name,should be unique to diff other data.
         * @param	void* data				- [in] the data buffer pointer, data could be binary.
         * @param	unsigned int dataLen	- [in] length of data.
         * @param	const char* params		- [in] parameters about uploading data.
         * @param	int* errorCode			- [out] Return 0 in success, otherwise return error code.
         * @see		
         */
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        static extern string QISRUploadData(string sessionID, string dataName, byte[] data, uint dataLen, string parameters, out MspErrors errorCode);

        /// <summary>
        /// QISRInit应当在应用程序初始化时仅调用一次。
        /// </summary>
        /// <param name="config"></param>
        public static void Init(IsrConfig config)
        {
            MspErrors result = QISRInit(config.ToString());
            switch (result)
            {
                case MspErrors.MSP_SUCCESS://成功
                    break;
                case MspErrors.MSP_ERROR_CONFIG_INITIALIZE:
                    throw new Exception("初始化ISR的config实例时出现错误。");
                default:
                    throw new Exception("QISR初始化失败，错误代码: " + result);
            }
        }

        public static void End()
        {
            MspErrors result = QISRFini();
            if(result != MspErrors.MSP_SUCCESS)
                throw new Exception("QISRFini failed, error code is " + result);
        }

        /// <summary>
        /// 本接口用来开始一路ISR会话，并在参数中指定本路ISR会话用到的语法列表，本次会话所用的参数等。
        /// </summary>
        /// <param name="grammarList"></param>
        /// <param name="_params"></param>
        /// <returns>会话ID。</returns>
        public static string SessionBegin(string grammarList, string _params)
        {
            MspErrors result = 0; 
            IntPtr ptr = QISRSessionBegin(grammarList, _params, out result);
            
            switch (result)
            {
                case MspErrors.MSP_SUCCESS://成功
                    return MarshalUtils.Ptr2Str(ptr);
                    break;
                case MspErrors.MSP_ERROR_NOT_INIT:
                    throw new Exception("未初始化。");
                case MspErrors.MSP_ERROR_INVALID_PARA:
                    throw new Exception("无效的参数。");
                case MspErrors.MSP_ERROR_NO_LICENSE:
                    throw new Exception("开始一路会话失败。");
                default:
                    throw new Exception("QISR会话初始化失败，错误代码: " + result);
            }
        }
        public static void AudioWrite(string sessionID, IntPtr waveData, uint waveLen, AudioStatus audioStatus, out EndPointStatusEnums epStatus, out RecogStatusEnums recogStatus)
        {
            MspErrors result = QISRAudioWrite(sessionID, waveData, waveLen, audioStatus, out epStatus, out recogStatus);
            CheckResult(result);
        }

        static void CheckResult(MspErrors code)
        {
            switch (code)
            {
                case MspErrors.MSP_SUCCESS://成功
                    return;
                case MspErrors.MSP_ERROR_NOT_INIT: throw new EngineNotInitException();
                case MspErrors.MSP_ERROR_INVALID_HANDLE: throw new EngineInvalidHandleException();
                case MspErrors.MSP_ERROR_INVALID_PARA: throw new EngineInvalidParaException();
                case MspErrors.MSP_ERROR_NO_DATA: throw new EngineNoDataException();
                case MspErrors.MSP_ERROR_INVALID_PARA_VALUE: throw new EngineInvalidParaValueException();
                case MspErrors.MSP_ERROR_NO_LICENSE: throw new EngineNoLicenseException();
            }
        }

        public static string GetResult(string sessionId, out RecogStatusEnums rsltStatus, int waitTime = 5000)
        {
            MspErrors result;
            IntPtr p = QISRGetResult(sessionId, out rsltStatus, waitTime, out result);
            CheckResult(result);
            if (p == IntPtr.Zero)
                return null;

            return MarshalUtils.Ptr2Str(p);
        }

        public static string UploadData(string sessionID, string keywords)
        {
            byte[] data = Encoding.UTF8.GetBytes(keywords);
            MspErrors result;
            string str = QISRUploadData(sessionID, "contact", data, (uint)data.Length, "dtt=keylist", out result);
            CheckResult(result);

            return str;
        }
    }
}
