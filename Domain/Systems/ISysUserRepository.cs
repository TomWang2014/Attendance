// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISysUserRepository.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/8 16:09:25</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Domain.Systems
{
    using System.Collections.Generic;

    using Attendance.Infrastructure.Data;

    /// <summary>
    /// 数据类别相关接口
    /// </summary>
    public interface ISysUserRepository : IRepositoryBase<NetSysUser>
    {
    }
}
