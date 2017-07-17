// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TenantSearch.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/17 14:56:54</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Application.SystemMgtServices.Dtos
{
    using Attendance.Infrastructure.Dto;

    /// <summary>
    /// UserSearch
    /// </summary>
    public class TenantSearch : EntityPagedDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 字段排序
        /// </summary>
        public string Orderby { get; set; }

        /// <summary>
        /// 字段排序
        /// </summary>
        public bool Desc { get; set; }
    }
}
