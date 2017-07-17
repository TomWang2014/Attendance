
using System;
using System.Collections.Generic;

using Attendance.Domain;
using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Dto;
namespace Attendance.Application.SystemMgtServices.Dtos
{


    /// <summary>
    /// 当前登录用户
    /// </summary>
    [Serializable]
    [AutoMap(typeof(NetSysUser))]
    public class CurrentUserDto : EntityDto<int>
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        public CurrentUserDto()
        {
            this.FuncItems = new List<SysFuncItem>();
            this.AuthenticationUrl = new List<string>();
        }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 用户所属权限
        /// </summary>
        public RoleNote NetSysRole { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public List<SysFuncItem> FuncItems { get; set; }

        /// <summary>
        /// 认证通过的Url
        /// </summary>
        public List<string> AuthenticationUrl { get; set; }

        /// <summary>
        /// 当前登录用户是否是租户
        /// </summary>
        public bool IsTenant { get; set; }

        /// <summary>
        /// 获得角色名称
        /// </summary>
        /// <returns>角色名称</returns>
        public override string ToString()
        {
            return this.NetSysRole == null ? string.Empty : this.NetSysRole.Name;
        }
    }
}
