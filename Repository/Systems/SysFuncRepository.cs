﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SysFuncRepository.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/8 16:15:31</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Repository.Systems
{
    using Attendance.Domain;
    using Attendance.Domain.Systems;
    using Attendance.Infrastructure.Data;

    /// <summary>
    /// ResDataCategoryRepository
    /// </summary>
    public class SysFuncRepository : RepositoryBase<NetSysFunc>, ISysFuncRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public SysFuncRepository(AppContext context)
            : base(context)
        {
        }
    }
}
