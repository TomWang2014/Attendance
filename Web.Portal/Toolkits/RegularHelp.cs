using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Attendance.Web.Portal.Toolkits
{
    public static class RegularHelp
    {
        /// <summary>
        /// 是否全部是汉字
        /// </summary>
        /// <param name="str">
        /// 待检测数据
        /// </param>
        /// <returns>
        /// 结果
        /// </returns>
        public static bool IsAllZhCn(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (Regex.IsMatch(str, @"^[\u4E00-\u9FA5]+$"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否包含汉字
        /// </summary>
        /// <param name="str">
        /// 待检测数据
        /// </param>
        /// <returns>
        /// 结果
        /// </returns>
        public static bool IsContainZhCn(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (Regex.IsMatch(str, @"[\u4e00-\u9fa5]"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来，
        /// 也可以不用，区号与本地号间可以用连字号或空格间隔，
        /// 也可以没有间隔
        /// \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(string input)
        {
            string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex(@"^1(3|4|5|7|8)\d{9}$");
            return regex.IsMatch(input);
        }

    }
}