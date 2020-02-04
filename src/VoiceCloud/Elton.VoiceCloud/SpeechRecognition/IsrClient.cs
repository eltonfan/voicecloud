// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace Elton.VoiceCloud.ISR
{
    /// <summary>
    /// 实现对讯飞 ISR 接口封装。
    /// </summary>
    public partial class IsrClient : IDisposable
    {
        readonly string recog_grammar = "builtin:grammar/../search/location.abnf?language=zh-cn";
        readonly string recog_params = null;
        /// <summary>
        /// 构造函数，初始化引擎，开Session
        /// </summary>
        /// <param name="config">初始化引擎的参数</param>
        /// <param name="sessionParameters">开session的参数</param>
        public IsrClient(IsrConfig config, string recog_grammar, string recog_params)
        {
            this.recog_grammar = recog_grammar;
            this.recog_params = recog_params;

            IsrInterop.Init(config);
        }

        public void Dispose()
        {
            IsrInterop.End();
        }

        public SpeechRecognitionSession CreateSession()
        {
            SpeechRecognitionSession session = new SpeechRecognitionSession(this.recog_grammar, this.recog_params);
            session.Open();

            return session;
        }
    }
}
