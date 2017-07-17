
using System.Collections.Generic;

using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Dto;
using Attendance.Domain;

namespace Attendance.Application.SystemMgtServices.Dtos
{

   

    /// <summary>
    /// TenantItem
    /// </summary>
    [AutoMap(typeof(NetTenant))]
    public class TenantItem : EntityDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TenantItem()
        {
            //this.NetTenantIpWhites = new List<IpNode> { new IpNode(), new IpNode(), new IpNode() };
        }

        /// <summary>
        /// 账号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string NetSysUserAccount { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string NetSysUserNetSysRoleName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string NetSysUserName { get; set; }

        /// <summary>
        /// 租户图标
        /// </summary>
        public string FrontImage { get; set; }

        /// <summary>
        /// 租户logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Descriptions { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int NetSysUserNetSysRoleId { get; set; }

        /// <summary>
        /// 租户对应UserId
        /// </summary>
        public int NetSysUserId { get; set; }
      
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

      
    }
}
