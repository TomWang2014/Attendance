//------------------------------------------------------------
// <copyright file="AuthorityUrls.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/14 14:08:28</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Web.Portal.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// 访问权限模型
    /// </summary>
    public class AuthorityUrls
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorityUrls()
        {
            this.IncludeUrl = new List<string>();
        }

        /// <summary>
        /// 权限
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 权限对应的访问url
        /// </summary>
        public List<string> IncludeUrl { get; set; }
    }
}