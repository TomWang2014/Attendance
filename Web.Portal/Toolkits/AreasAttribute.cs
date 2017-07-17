//------------------------------------------------------------
// <copyright file="AreasAttribute.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/19 13:05:14</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using System;

    /// <summary>
    /// 域控制器特性类
    /// </summary>
    public class AreasAttribute : Attribute
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
    }
}