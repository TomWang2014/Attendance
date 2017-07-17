// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/6/17 13:45:14</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    using Attendance.Infrastructure.Entity;

    /// <summary>
    /// The enum helper.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举下拉绑定数据源
        /// </summary>
        /// <param name="enumType">
        /// 枚举类型
        /// </param>
        /// <returns>
        /// 返回ComboboxItem列表
        /// </returns>
        public static IEnumerable<ComboboxItemDto> EnumComboboxItem(Type enumType)
        {
            var dic = EnumHelper.EnumListDictionary(enumType);

            return from a in dic
                   select new ComboboxItemDto
                   {
                       Value = a.Value, 
                       DisplayText = a.Key
                   };
        }

        /// <summary>
        /// 获得某个Enum类型的绑定列表
        /// </summary>
        /// <param name="enumType">
        /// 枚举的类型，例如：typeof(Sex)
        /// </param>
        /// <returns>
        /// 返回一个DataTable
        /// DataTable 有两列：    "Text"    : System.String;
        ///                        "Value"    : System.Char
        /// </returns>
        public static DataTable EnumListTable(Type enumType)
        {
            if (enumType.IsEnum != true)
            {    // 不是枚举的要报错
                throw new InvalidOperationException();
            }

            // 建立DataTable的列信息
            var dt = new DataTable();
            dt.Columns.Add("Text", typeof(System.String));
            dt.Columns.Add("Value", typeof(System.Char));

            // 获得特性Description的类型信息
            var typeDescription = typeof(DescriptionAttribute);

            // 获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            var fields = enumType.GetFields();

            // 检索所有字段
            foreach (var field in fields)
            {
                // 过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {
                    var dr = dt.NewRow();

                    // 通过字段的名字得到枚举的值

                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内
                    dr["Value"] = (char)(int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                    // 获得这个字段的所有自定义特性，这里只查找Description特性
                    var arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        // 因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                        var aa = (DescriptionAttribute)arr[0];

                        // 获得特性的描述值，也就是‘男’‘女’等中文描述
                        dr["Text"] = aa.Description;
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// 获得某个Enum类型的绑定列表
        /// </summary>
        /// <param name="enumType">
        /// 枚举的类型，例如：typeof(Sex)
        /// </param>
        /// <returns>
        /// 返回一个Dictionary
        /// </returns>
        public static Dictionary<string, string> EnumListDictionary(Type enumType)
        {
            if (enumType.IsEnum != true)
            {    // 不是枚举的要报错
                throw new InvalidOperationException();
            }


            var dic = new Dictionary<string, string>();

            // 获得特性Description的类型信息
            var typeDescription = typeof(DescriptionAttribute);

            // 获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            var fields = enumType.GetFields();

            // 检索所有字段
            foreach (var field in fields)
            {
                // 过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {

                    // 通过字段的名字得到枚举的值

                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内

                    // r["Value"] = (char)(int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                    var value = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    string key;

                    // 获得这个字段的所有自定义特性，这里只查找Description特性
                    var arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        // 因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                        var aa = (DescriptionAttribute)arr[0];

                        // 获得特性的描述值，也就是‘男’‘女’等中文描述
                        // dr["Text"] = aa.Description;
                        key = aa.Description;
                        dic.Add(key, value);
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// 根据枚举value返回该value的Description，如果Description不存在，则返回该value的key
        /// </summary>
        /// <param name="enumType">
        /// The enum Type.
        /// </param>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDescriptionByValue(Type enumType, string value)
        {
            if (enumType.IsEnum != true)
            {    // 不是枚举的要报错
                throw new InvalidOperationException();
            }


            var desciption = string.Empty;

            // 获得特性Description的类型信息
            var typeDescription = typeof(DescriptionAttribute);

            // 获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            var fields = enumType.GetFields();

            // 检索所有字段
            foreach (var field in fields)
            {
                // 过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {

                    // 通过字段的名字得到枚举的值

                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内

                    // r["Value"] = (char)(int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                    if (value == ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString())
                    {
                        // 获得这个字段的所有自定义特性，这里只查找Description特性
                        var arr = field.GetCustomAttributes(typeDescription, true);
                        if (arr.Length > 0)
                        {
                            // 因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                            var aa = (DescriptionAttribute)arr[0];

                            // 获得特性的描述值，也就是‘男’‘女’等中文描述
                            // dr["Text"] = aa.Description;
                            desciption = aa.Description;
                        }
                        else
                        {
                            // 如果没有特性描述（-_-!忘记写了吧~）那么就显示英文的字段名
                            desciption = field.Name;
                        }

                        break;
                    }

                }
            }

            return desciption;
        }

        /// <summary>
        /// 获得枚举的值
        /// </summary>
        /// <param name="euums">
        /// 枚举类型
        /// </param>
        /// <returns>
        /// 返回枚举对应的int值
        /// </returns>
        public static int EnumValue(this object euums)
        {
            return Convert.ToInt32(euums);
        }

        /// <summary>
        /// 获得枚举的文本描述
        /// </summary>
        /// <param name="euums">
        /// 枚举类型
        /// </param>
        /// <returns>
        /// 返回枚举对应的string值
        /// </returns>
        public static string EnumText(this object euums)
        {
            return euums.ToString();
        }

        /// <summary>
        /// 返回该枚举是否和str文本值相等
        /// </summary>
        /// <param name="enumType">
        /// 枚举值
        /// </param>
        /// <param name="str">
        /// 比较的字符串
        /// </param>
        /// <returns>
        /// 是否相等
        /// </returns>
        public static bool EqualsEnumString(this object enumType, string str)
        {
            return enumType.ToString().Equals(str);
        }

        /// <summary>
        /// 返回该枚举是否和str文本值相等
        /// </summary>
        /// <param name="str">
        /// 比较的字符串 
        /// </param>
        /// <param name="enumType">
        /// 枚举值 
        /// </param>
        /// <returns>
        /// 是否相等 
        /// </returns>
        public static bool EqualsEnumString(this string str, object enumType)
        {
            return enumType.ToString().Equals(str);
        }

        /// <summary>
        /// 返回该数字（int）是否和枚举相等
        /// </summary>
        /// <param name="enumValue">
        /// 值（int）
        /// </param>
        /// <param name="enumType">
        /// 枚举类型
        /// </param>
        /// <returns>
        /// 是否相等
        /// </returns>
        public static bool EqualsEnumValue(this int enumValue, object enumType)
        {
            return enumType.EnumValue() == enumValue;
        }

        /// <summary>
        /// 返回该数字（int）是否和枚举相等
        /// </summary>
        /// <param name="enumType">
        /// 枚举类型 
        /// </param>
        /// <param name="enumValue">
        /// 值（int） 
        /// </param>
        /// <returns>
        /// 是否相等 
        /// </returns>
        public static bool EqualsEnumValue(this object enumType, int enumValue)
        {
            return enumType.EnumValue() == enumValue;
        }
    }
}
