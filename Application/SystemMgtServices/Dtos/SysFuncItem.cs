//------------------------------------------------------------
// <copyright file="SysFuncItem.cs" company="zjzx">
//    ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/9 17:17:49</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------
using Attendance.Domain;
namespace Attendance.Application.SystemMgtServices.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    using Attendance.Infrastructure.AutoMapper;
    using Attendance.Infrastructure.Toolkit;

    using MongoDB.Driver.Linq;

    /// <summary>
    /// SysFuncItem
    /// </summary>
    [Serializable]
    [AutoMap(typeof(NetSysFunc))]
    public class SysFuncItem
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
        /// 排序编号
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// 功能路径
        /// </summary>
        public string Url { get; set; }

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
        /// 父功能Id
        /// </summary>
        public int? ParentFuncId { get; set; }

        /// <summary>
        /// 功能编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int FuncType { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 子权限
        /// </summary>
        public List<FuncSmall> NetSysFunc1 { get; set; }

        /// <summary>
        /// 子权限
        /// </summary>
        public List<FuncSmall> SubSysFunc
        {
            get
            {
                return this.NetSysFunc1;
            }
        }

        /// <summary>
        /// 获得完整路径
        /// </summary>
        /// <returns>返回url</returns>
        public override string ToString()
        {
            return StringUrlExtension.GetRequestUrlByParameter(this.AreaName, this.ControllerName, this.ActionName);
        }
    }
}
