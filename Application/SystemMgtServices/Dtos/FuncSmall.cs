using System;
using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Toolkit;
using Attendance.Domain;
namespace Attendance.Application.SystemMgtServices.Dtos
{
    

    /// <summary>
    /// The func small.
    /// </summary>
    [Serializable]
    [AutoMap(typeof(NetSysFunc))]
    public class FuncSmall
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 访问Action名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 访问Controller名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 是否是租户的权限
        /// </summary>
        public bool? IsTenantFunc { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获得完整路径
        /// </summary>
        /// <returns>返回url</returns>
        public override string ToString()
        {
            return StringUrlExtension.GetRequestUrlByParameter(this.AreaName, this.ControllerName, this.ActionName);
        }

        public bool IsDisplay { get; set; }
    }
}