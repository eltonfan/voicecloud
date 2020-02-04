// Coded by chuangen http://chuangen.name.

using Elton.VoiceCloud.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud
{
    /// <summary>
    /// 初始化时传入的字符串，以指定识别或听写用到的一些配置参数。
    /// </summary>
    public class TtsConfig
    {
        readonly static Dictionary<string, string> defaultValues = new Dictionary<string, string>();
        static TtsConfig()
        {
            defaultValues.Add("server_url", "dev.voicecloud.cn:80/index.htm");
            defaultValues.Add("appid", "");
            defaultValues.Add("timeout", "30000");
            defaultValues.Add("coding_libs", "");
            defaultValues.Add("max_text_size", "4096");
        }

        readonly Dictionary<string, string> settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public TtsConfig()
        {
            foreach (KeyValuePair<string, string> pair in defaultValues)
                settings[pair.Key] = pair.Value;
        }
        protected string GetString(string key)
        {
            if (!settings.ContainsKey(key))
                return "";
            return settings[key];
        }

        protected void SetString(string key, string value)
        {
            if (settings.ContainsKey(key))
                settings[key] = value;
            else
                settings.Add(key, value);
        }

        protected Int32 GetInt32(string key)
        {
            if(!settings.ContainsKey(key))
                return 0;
            Int32 value;
            if (!Int32.TryParse(settings[key], out value))
                return 0;
            return value;
        }

        protected void SetInt32(string key, Int32 value)
        {
            this.SetString(key, value.ToString());
        }

        protected Boolean GetBoolean(string key)
        {
            if (!settings.ContainsKey(key))
                return false;
            Boolean value;
            if (!Boolean.TryParse(settings[key], out value))
                return false;
            return value;
        }

        protected void SetBoolean(string key, Boolean value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// MSP服务器URL，格式：域名:端口号/资源名，如dev.voicecloud.cn:80/index.htm。
        /// 默认值为：dev.voicecloud.cn:80/index.htm。
        /// </summary>
        public string ServerUrl
        {
            get { return GetString("server_url"); }
            set { SetString("server_url", value); }
        }

        /// <summary>
        /// 应用程序ID，服务端根据此参数跟踪应用程序信息，需从http://dev.voicecloud.cn/myapp_reg.php 页面申请。
        /// </summary>
        public string ApplicationId
        {
            get { return GetString("appid"); }
            set { SetString("appid", value); }
        }

        /// <summary>
        /// 超时间隔，单位毫秒，缺省值为30000。
        /// </summary>
        public int Timeout
        {
            get { return GetInt32("timeout"); }
            set { SetInt32("timeout", value); }
        }

        /// <summary>
        /// 音频压缩所用编解码库的名字，可用的有amr.dll、amr_fx.dl、amr_wb.dll、amr_wb_fx.dll和speex.dll，默认值为speex.dll。
        /// 同时加载多个编解码库时各个编解码库用“;”隔开，如：coding_libs=speex.dll; amr-wb.dll。
        /// </summary>
        public string CodingLibs
        {
            get { return GetString("coding_libs"); }
            set { SetString("coding_libs", value); }
        }

        /// <summary>
        /// 最大合成文本长度0-4096
        /// </summary>
        public int MaxTextSize
        {
            get { return GetInt32("max_text_size"); }
            set { SetInt32("max_text_size", value); }
        }
        
        /// <summary>
        /// 初始化时传入的字符串，以指定识别或听写用到的一些配置参数，
        /// 各个参数以“参数名=参数值”的形式出现，大小写不敏感，不同的参数之间以“,”或“\n”隔开
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<string, string> pair in this.settings)
            {
                if (pair.Value == defaultValues[pair.Key])//如果是默认值，则无需添加
                    continue;

                if (first)
                    first = false;
                else
                    sb.Append(",");
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(pair.Value);
            }

            return sb.ToString();
        }
    }
}
