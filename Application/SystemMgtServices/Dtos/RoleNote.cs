using System;

using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Dto;
using Attendance.Domain;

namespace Attendance.Application.SystemMgtServices.Dtos
{


    /// <summary>
    /// RoleNote
    /// </summary>
    [Serializable]
    [AutoMap(typeof(NetSysRole))]
    public class RoleNote : EntityDto
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
    }
}
