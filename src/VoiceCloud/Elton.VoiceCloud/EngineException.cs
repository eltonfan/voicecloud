// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud
{
    /// <summary>
    /// 语音引擎异常。
    /// </summary>
    public class EngineException : Exception
    {
        public MspErrors Result { get; private set; }
        /// <summary>
        /// 初始化 System.Exception 类的新实例。
        /// </summary>
        /// <param name="result"></param>
        public EngineException(MspErrors result)
            : base(EnumHelper.GetName(typeof(MspErrors), result))
        {
            this.Result = result;
        }
        /// <summary>
        /// 使用指定的错误消息初始化 System.Exception 类的新实例。
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message">描述错误的消息。</param>
        public EngineException(MspErrors result, string message)
            : base(message)
        {
            this.Result = result;
        }
        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 System.Exception 类的新实例。
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public EngineException(MspErrors result, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Result = result;
        }
    }

    /// <summary>
    /// 未初始化。
    /// </summary>
    public class EngineNotInitException : EngineException
    {
        public EngineNotInitException()
            : base(MspErrors.MSP_ERROR_NOT_INIT)
        { }
    }
    /// <summary>
    /// 无效的会话ID。
    /// </summary>
    public class EngineInvalidHandleException : EngineException
    {
        public EngineInvalidHandleException()
            : base(MspErrors.MSP_ERROR_INVALID_HANDLE)
        { }
    }
    /// <summary>
    /// 无效的参数。
    /// </summary>
    public class EngineInvalidParaException : EngineException
    {
        public EngineInvalidParaException()
            : base(MspErrors.MSP_ERROR_INVALID_PARA)
        { }
    }
    /// <summary>
    /// 没有数据（如没有写入识别所用的音频等）。
    /// </summary>
    public class EngineNoDataException : EngineException
    {
        public EngineNoDataException()
            : base(MspErrors.MSP_ERROR_NO_DATA)
        { }
    }
    /// <summary>
    /// 无效的参数值。
    /// </summary>
    public class EngineInvalidParaValueException : EngineException
    {
        public EngineInvalidParaValueException()
            : base(MspErrors.MSP_ERROR_INVALID_PARA_VALUE)
        { }
    }
    /// <summary>
    /// 会话模式中此前开始一路会话失败。
    /// </summary>
    public class EngineNoLicenseException : EngineException
    {
        public EngineNoLicenseException()
            : base(MspErrors.MSP_ERROR_NO_LICENSE)
        { }
    }
}
