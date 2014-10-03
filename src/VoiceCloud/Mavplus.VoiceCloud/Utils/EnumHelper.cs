// Coded by chuan'gen http://chuangen.name.

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Mavplus
{
    /// <summary>
    /// 枚举辅助类。可以利用枚举的DescriptionAttribute进行名称和值的转换。
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static string[] GetNames(Type enumType)
        {
            if(enumType == null)
                throw new ArgumentNullException("enumType", "enumType 为 null。");
            if(!enumType.IsEnum)
                throw new ArgumentException("enumType", "enumType 参数不是 System.Enum。");

            List<string> names = new List<string>();
            foreach(FieldInfo field in enumType.GetFields())
            {
                if(field.FieldType != enumType)
                    continue;

                // Get the stringvalue attributes
                DescriptionAttribute[] attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                string name = attribs.Length > 0 ? attribs[0].Description : field.Name;//返回Description 或 字段名称。
                if(!string.IsNullOrEmpty(name))
                    names.Add(name);
            }
            return names.ToArray();
        }

        static string GetName(FieldInfo field)
        {
            DescriptionAttribute[] attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            string name = attribs.Length > 0 ? attribs[0].Description : field.Name;//返回StringValue 或 字段名称。
            return name;
        }
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetName(Type enumType, object value)
        {
            // Get fieldinfo for this type
            FieldInfo field = enumType.GetField(value.ToString());
            if (field == null)
            {//可能是Flags标记的枚举，可按位计算
                return value.ToString();
            }
            return GetName(field);
        }
        /// <summary>
        /// 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。
        /// </summary>
        /// <param name="enumType">枚举的 System.Type。</param>
        /// <param name="value">包含要转换的值或名称的字符串。</param>
        /// <returns>enumType 类型的对象，其值由 value 表示。</returns>
        public static object Parse(Type enumType, string value)
        {
            return Parse(enumType, value, true);
        }
        /// <summary>
        /// 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。一个参数指定该操作是否区分大小写。
        /// </summary>
        /// <param name="enumType">枚举的 System.Type。</param>
        /// <param name="value">包含要转换的值或名称的字符串。</param>
        /// <param name="ignoreCase">如果为 true，则忽略大小写；否则考虑大小写。</param>
        /// <returns>enumType 类型的对象，其值由 value 表示。</returns>
        public static object Parse(Type enumType, string value, bool ignoreCase)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType", "enumType 为 null。");
            if (!enumType.IsEnum)
                throw new ArgumentException("enumType", "enumType 参数不是 System.Enum。");

            object result = null;
            foreach (FieldInfo field in enumType.GetFields())
            {
                string name = GetName(field);
                if (string.Equals(name, value, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture))
                    result = field.GetRawConstantValue();
            }
            return result;
        }
    }
}
