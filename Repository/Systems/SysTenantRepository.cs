// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SysTenantRepository.cs" company="zjzx">
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
    using System.Collections.Generic;
    using System.Linq;

    using Attendance.Domain;
    using Attendance.Domain.Systems;
    using Attendance.Infrastructure.Data;

    /// <summary>
    /// 用户管理仓储
    /// </summary>
    public class SysTenantRepository : RepositoryBase<NetTenant>, ISysTenantRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public SysTenantRepository(AppContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 获得所有用户信息
        /// </summary>
        /// <returns>返回用户信息</returns>
        public List<NetSysUser> GetAllTenantByRepositoryToSql()
        {
            var list = this.DataContext.Database.SqlQuery<NetSysUser>("select * from NetSysUser").ToList();
            return list;
        }
    }
}
