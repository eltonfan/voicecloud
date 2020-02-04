// Coded by chuangen http://chuangen.name.

using Elton.VoiceCloud.ISR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud
{
    /// <summary>
    /// 初始化时传入的字符串，以指定识别或听写用到的一些配置参数。
    /// </summary>
    public class IsrConfig
    {
        readonly static Dictionary<string, string> defaultValues = new Dictionary<string, string>();
        static IsrConfig()
        {
            defaultValues.Add("server_url", "dev.voicecloud.cn:80/index.htm");
            defaultValues.Add("appid", "");
            defaultValues.Add("timeout", "30000");
            defaultValues.Add("coding_libs", "");
            defaultValues.Add("max_audio_size", "262144");
            defaultValues.Add("coding_chunk_size", "5120");
            defaultValues.Add("vad_enable", "true");
            defaultValues.Add("audio_coding", "speex-wb");
            defaultValues.Add("conding_level", "7");
        }

        readonly Dictionary<string, string> settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public IsrConfig()
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

        [Obsolete("尚未实现。")]
        public CodingLibEnums CodingLibs2
        {
            get { return CodingLibEnums.SpeexDll; }
            set { }
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
        /// 最大音频长度，单位为Byte，最大值为1MB，缺省值为262144（256KB）。
        /// </summary>
        public int MaxAudioSize
        {
            get { return GetInt32("max_audio_size"); }
            set { SetInt32("max_audio_size", value); }
        }

        /// <summary>
        /// MSC压缩音频时（在用户还没有将音频发送完毕时）每次写入编解码库的音频大小，
        /// 单位为Byte，最大值为max_audio_size，默认值为5120。
        /// </summary>
        public int CodingChunkSize
        {
            get { return GetInt32("coding_chunk_size"); }
            set { SetInt32("coding_chunk_size", value); }
        }

        /// <summary>
        /// 打开或关闭MSC内部集成的VAD功能。此配置项取值为true或false（1或0），默认为true（1）。
        /// MSC集成的VAD主要功能有本地语音前后端点检测、静音去除和降噪等，这些功能只有在VAD功能被打开时才能使用。
        /// 相对于使用服务器端的此类功能，本地前后端点检测更灵敏（服务器响应有延迟），静音去除和降噪可以减少发往服务器的数据量。
        /// </summary>
        public bool vad_enable
        {
            get { return GetBoolean("vad_enable"); }
            set { SetBoolean("vad_enable", value); }
        }

        /// <summary>
        /// 编解码算法，可取的值为speex、speex-wb、amr、amr-fx、amr-wb、amr-wb-fx、raw，默认值为speex-wb。
        /// 使用某一种编解码算法时必须在coding_libs配置项中加载了对应的编解码库，编解码算法和编解码库的对应关系是：
        /// speex-wb & speex: speex.dll
        /// amr: amr.dll
        /// amr-fx: amr_fx.dll
        /// amr-wb: amr_wb.dll
        /// amr-wb-fx: amr_wb_fx.dll
        /// raw：不需要加载动态库
        /// </summary>
        public string audio_coding
        {
            get { return GetString("audio_coding"); }
            set { SetString("audio_coding", value); }
        }

        /// <summary>
        /// 音频压缩编码等级。amr系列算法取值范围为-1-7，speex系列为-1-10。
        /// 默认值为7。等级为-1的含义是写入MSC的是（已经）压缩的音频
        /// </summary>
        public int conding_level
        {
            get { return GetInt32("conding_level"); }
            set { SetInt32("conding_level", value); }
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
