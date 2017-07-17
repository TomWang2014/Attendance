//------------------------------------------------------------
// <copyright file="EntityActionsDto.cs" company="zjzx">
//    ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/21 13:39:33</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Infrastructure.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// EntityActionsDto
    /// </summary>
    public class EntityActionsDto
    {
        /// <summary>
        /// 区域名
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 最终Url
        /// </summary>
        public string Url { get; set; }
    }
}
