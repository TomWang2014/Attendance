// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserItem.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/10 15:18:49</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

using Attendance.Domain;
using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Dto;
using Attendance.Infrastructure.Validation;

namespace Attendance.Application.SystemMgtServices.Dtos
{

    /// <summary>
    /// AddUserItem
    /// </summary>
    [AutoMap(typeof(NetSysUser))]
    public class UserItem : EntityDto, IValidate
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        public UserItem()
        {
            this.IsDeleted = false;
            
            // 默认用户类型是1
            this.Type = 1;
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Required(ErrorMessage = " 用户账号是必填的。")]
        public string Account { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required(ErrorMessage = " 用户昵称是必填的。")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// 所属角色名称
        /// </summary>
        public string Net_SysRoleName { get; set; }

        /// <summary>
        /// 所属权限
        /// </summary>
        public long? SysRoleId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int Type { get; set; }
    }
}
