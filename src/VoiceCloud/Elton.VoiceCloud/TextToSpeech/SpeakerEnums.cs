// Coded by chuangen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Elton.VoiceCloud.TextToSpeech
{
    /// <summary>
    /// speeker朗读者枚举常量，命名不可变更。
    /// </summary>
    public enum SpeakerEnums
    {
        Unknown = 0,

        [Description("小燕 青年女声 中英文 普通话")]
        xiaoyan,
        [Description("小宇 青年男声 中英文 普通话")]
        xiaoyu,
        [Description("凯瑟琳 青年女声 英语")]
        catherine,
        [Description("亨利 青年男声 英语")]
        henry,
        [Description("玛丽 青年女声 英语")]
        mary,
        [Description("小研 青年女声 中英文 普通话")]
        xy,
        [Description("小琪 青年女声 中英文 普通话")]
        xq,
        [Description("小峰 青年男声 中英文 普通话")]
        xf,
        [Description("小梅 青年女声 中英文 粤语")]
        xm,
        [Description("小莉 青年女声 中英文 台普")]
        xl,
        [Description("小蓉 青年女声 汉语 四川话")]
        xr,
        [Description("小芸 青年女声 汉语 东北话")]
        xyun,
        [Description("小坤 青年男声 汉语 河南话")]
        xk,
        [Description("小强 青年男声 汉语 湖南话")]
        xqa,
        [Description("小莹 青年女声 汉语 陕西话")]
        xying,
        [Description("小新 童年男声 汉语 普通话")]
        xx,
        [Description("楠楠 童年女声 汉语 普通话")]
        nn,
        [Description("老孙 老年男声 汉语 普通话")]
        ls,
        [Description("玛丽安 青年女声 法语")]
        Mariane,
        [Description("古丽 青年女声 维吾尔语")]
        Guli,
        [Description("阿拉本 青年女声 俄罗斯语")]
        Allabent,
        [Description("加芙列拉 青年女声 西班牙语")]
        Gabriela,
        [Description("艾伯哈 青年女声 印地语")]
        Abha,
        [Description("小云 青年女声 越南语")]
        XiaoYun,
    }

    public class SpeakerInfo
    {
        public SpeakerEnums Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Expression { get; private set; }
        private SpeakerInfo(SpeakerEnums id, string name, string expression)
        {
            this.Id = id;
            this.Name = name;
            this.DisplayName = "";
            this.Expression = expression;
        }

        public override string ToString()
        {
 	         return this.DisplayName;
        }

        static List<SpeakerInfo> list = null;
        static Dictionary<SpeakerEnums, SpeakerInfo> dic1 = null;
        static Dictionary<string, SpeakerInfo> dic2 = null;

        static void Initialize()
        {
            list = new List<SpeakerInfo>();
            list.Add(new SpeakerInfo(SpeakerEnums.xiaoyan, "xiaoyan", "ent=intp65,vcn=xiaoyan"));
            list.Add(new SpeakerInfo(SpeakerEnums.xiaoyu, "xiaoyu", "ent=intp65,vcn=xiaoyu"));
            list.Add(new SpeakerInfo(SpeakerEnums.catherine, "catherine", "ent=intp65_en,vcn=Catherine"));
            list.Add(new SpeakerInfo(SpeakerEnums.henry, "henry", "ent=intp65_en,vcn=henry"));
            list.Add(new SpeakerInfo(SpeakerEnums.mary, "mary", "ent=vivi21,vcn=vimary"));
            list.Add(new SpeakerInfo(SpeakerEnums.xy, "xy", "ent=vivi21,vcn=vixy"));
            list.Add(new SpeakerInfo(SpeakerEnums.xq, "xq", "ent=vivi21,vcn=vixq"));
            list.Add(new SpeakerInfo(SpeakerEnums.xf, "xf", "ent=vivi21,vcn=vixf"));
            list.Add(new SpeakerInfo(SpeakerEnums.xm, "xm", "ent=vivi21,vcn=vixm"));
            list.Add(new SpeakerInfo(SpeakerEnums.xl, "xl", "ent=vivi21,vcn=vixl"));
            list.Add(new SpeakerInfo(SpeakerEnums.xr, "xr", "ent=vivi21,vcn=vixr"));
            list.Add(new SpeakerInfo(SpeakerEnums.xyun, "xyun", "ent=vivi21,vcn=vixyun"));
            list.Add(new SpeakerInfo(SpeakerEnums.xk, "xk", "ent=vivi21,vcn=vixk"));
            list.Add(new SpeakerInfo(SpeakerEnums.xqa, "xqa", "ent=vivi21,vcn=vixqa"));
            list.Add(new SpeakerInfo(SpeakerEnums.xying, "xying", "ent=vivi21,vcn=vixying"));
            list.Add(new SpeakerInfo(SpeakerEnums.xx, "xx", "ent=vivi21,vcn=vixx"));
            list.Add(new SpeakerInfo(SpeakerEnums.nn, "nn", "ent=vivi21,vcn=vinn"));
            list.Add(new SpeakerInfo(SpeakerEnums.ls, "ls", "ent=vivi21,vcn=vils"));
            list.Add(new SpeakerInfo(SpeakerEnums.Mariane, "Mariane", "ent=mtts,vcn=Mariane"));
            list.Add(new SpeakerInfo(SpeakerEnums.Guli, "Guli", "ent=mtts,vcn=Guli"));
            list.Add(new SpeakerInfo(SpeakerEnums.Allabent, "Allabent", "ent=mtts,vcn=Allabent"));
            list.Add(new SpeakerInfo(SpeakerEnums.Gabriela, "Gabriela", "ent=mtts,vcn=Gabriela"));
            list.Add(new SpeakerInfo(SpeakerEnums.Abha, "Abha", "ent=mtts,vcn=Abha"));
            list.Add(new SpeakerInfo(SpeakerEnums.XiaoYun, "XiaoYun", "ent=mtts,vcn=XiaoYun"));

            dic1 = new Dictionary<SpeakerEnums, SpeakerInfo>();
            dic2 = new Dictionary<string, SpeakerInfo>();
            foreach (SpeakerInfo item in list)
            {
                item.DisplayName = EnumHelper.GetName(typeof(SpeakerEnums), item.Id);

                dic1.Add(item.Id, item);
                dic2.Add(item.Name, item);
            }
        }

        public static IList<SpeakerInfo> GetList()
        {
            if (list == null)
                Initialize();

            return list;
        }

        public static string GetExpression(SpeakerEnums value)
        {
            if (list == null)
                Initialize();

            if (!dic1.ContainsKey(value))
                return null;
            return dic1[value].Expression;
        }
        public static string GetName(SpeakerEnums value)
        {
            if (list == null)
                Initialize();

            if (!dic1.ContainsKey(value))
                return null;
            return dic1[value].Name;
        }

        public static SpeakerEnums Parse(string name)
        {
            if (list == null)
                Initialize();

            if (!dic2.ContainsKey(name))
                return SpeakerEnums.Unknown;

            return dic2[name].Id;
        }
    }
}
