using Attendance.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.SystemMgtServices.Dtos
{
    public class SignUpListSearch : EntityPagedDto
    {
        /// <summary>
        /// 合作机构id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 报名开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 报名结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 字段排序
        /// </summary>
        public string Orderby { get; set; }

        /// <summary>
        /// 字段排序
        /// </summary>
        public bool Desc { get; set; }
    }
}
