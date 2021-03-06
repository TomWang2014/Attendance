﻿//------------------------------------------------------------
// <copyright file="SysUserType.cs" company="zjzx">
//    ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/12/2 13:10:36</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Application.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// 角色类型
    /// </summary>
    public enum SysRoleType
    {
        /// <summary>
        /// 网络学院普通用户账号
        /// </summary>
        [Description("SysCommon")]
        SysCommon = 0,

        /// <summary>
        /// admin超级管理员角色
        /// </summary>
        [Description("SysAdmin")]
        SysAdmin = 1,

        /// <summary>
        /// 租户角色
        /// </summary>
        [Description("SysTenant")]
        SysTenant = 2
    }
}
