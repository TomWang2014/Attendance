using Attendance.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class TimeListSearch : EntityPagedDto
    {
        public int TrainId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Orderby { get; set; }

        /// <summary>
        /// 字段排序
        /// </summary>
        public bool Desc { get; set; }
    }
}
