// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppAuthorize.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/8/19 13:06:29</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc.Authorization
{
    using System;
    using System.Web;
    using System.Web.Security;

    /// <summary>
    /// AppAuthorize
    /// </summary>
    public class AppAuthorize
    {
        /// <summary>
        /// 统一退出
        /// </summary>
        /// <param name="request">
        /// request
        /// </param>
        /// <param name="response">
        /// response
        /// </param>
        public static void SignleSignOut(HttpRequestBase request, HttpResponseBase response)
        {
            FormsAuthentication.SignOut();

            var cookies = request.Cookies.AllKeys;

            foreach (var cookie in cookies)
            {
                var c = response.Cookies[cookie];
                if (c != null)
                {
                    c.Domain = FormsAuthentication.CookieDomain;
                    c.Expires = DateTime.Now.AddDays(-1);
                    response.Cookies.Add(c);
                }
            }
        }
    }
}
