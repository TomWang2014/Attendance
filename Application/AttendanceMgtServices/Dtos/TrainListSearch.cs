

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    using Attendance.Infrastructure.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class TrainListSearch : EntityPagedDto
    {
        /// <summary>
        /// 培训班名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间-开始
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// 创建时间-结束
        /// </summary>
        public string End { get; set; }

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
