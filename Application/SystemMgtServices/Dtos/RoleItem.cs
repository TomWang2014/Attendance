using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

using Attendance.Domain;
using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Dto;

using MongoDB.Driver.Linq;

namespace Attendance.Application.SystemMgtServices.Dtos
{
 

    /// <summary>
    /// 权限dto
    /// </summary>
    [AutoMap(typeof(NetSysRole))]
    public class RoleItem : EntityDto
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// 所属用户数量
        /// </summary>
        public int NetSysUsersCount { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        [ScriptIgnore]
        public List<SysFuncItem> NetSysFuncs { get; set; }

        /// <summary>
        /// 角色Id集合
        /// </summary>
        public List<int> SysFuncIds
        {
            get
            {
                var list = new List<int>();

                if (this.NetSysFuncs == null || this.NetSysFuncs.Count <= 0)
                {
                    return list;
                }

                foreach (var func in this.NetSysFuncs)
                {
                    if (func.FuncType == 2)
                    {
                        list.Add(func.Id);
                    }
                    //list.AddRange(func.NetSysFunc1.Select(funcSmall => funcSmall.Id));
                }

                return list;
            }
        }
    }
}
