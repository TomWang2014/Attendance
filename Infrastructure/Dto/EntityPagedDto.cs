//------------------------------------------------------------
// <copyright file="EntityPagedDto.cs" company="zjzx">
//    ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/17 14:58:28</date>
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
    /// EntityPagedDto
    /// </summary>
    public class EntityPagedDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public int PageSize { get; set; }

    }
}
