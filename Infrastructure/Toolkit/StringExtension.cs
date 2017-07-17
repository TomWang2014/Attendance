// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="zjzx">
//   2016-12-16 10:35:52
// </copyright>
// <author>李天赐</author>
// <date>2016/11/15 10:44:10</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Toolkit
{
    using System.Linq;

    /// <summary>
    /// StringExtension
    /// </summary>
    public static class StringUrlExtension
    {
        /// <summary>
        /// 根据参数获得请求完整的url
        /// </summary>
        /// <param name="areaName">
        /// 区域名称
        /// </param>
        /// <param name="controllerName">
        /// 控制器名称
        /// </param>
        /// <param name="actionName">
        /// actin名称
        /// </param>
        /// <returns>
        /// 返回完整路径
        /// </returns>
        public static string GetRequestUrlByParameter(string areaName, string controllerName, string actionName)
        {
            var requestUrl = string.IsNullOrEmpty(areaName)
                                 ? string.Format("/{0}/{1}", controllerName, actionName).ToLower()
                                 : string.Format("/{0}/{1}/{2}", areaName, controllerName, actionName).ToLower();

            return requestUrl;
        }

        /// <summary>
        /// The check ip address.
        /// </summary>
        /// <param name="ipAddress">
        /// The ip address.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsValidIp(string ipAddress)
        {
            // 如果什么没有等于无效
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            var ip = ipAddress.Trim();

            // 用"."把地址分开，如果分成的份数不是4份等于无效
            var numbers = ip.Split('.');

            if (numbers.Length != 4)
            {
                return false;
            }

            var num = 0;

            // 如果分成的四份中有任何一份不是整数等于无效
            if (numbers.Any(n => !int.TryParse(n, out num)))
            {
                return false;
            }

            // 如果数字大于255或者小于0等于无效
            if (numbers.Any(n => (int.Parse(n) > 255) || (int.Parse(n) < 0)))
            {
                return false;
            }

            // 如果第一份是0等于无效
            return int.Parse(numbers[0]) != 0;
        }
    }
}
