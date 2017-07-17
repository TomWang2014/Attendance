

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    using Attendance.Infrastructure.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class StudentListSearch : EntityPagedDto
    {
        /// <summary>
        /// 培训班
        /// </summary>
        public int TrainId { get; set; }
        /// <summary>
        /// 学员卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 学员手机号
        /// </summary>
        public string StudentPhone { get; set; }

        /// <summary>
        /// 报名时间-开始
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 报名时间-结束
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
