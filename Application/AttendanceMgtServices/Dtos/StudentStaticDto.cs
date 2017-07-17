using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class StudentStaticDto
    {
        /// <summary>
        /// 培训班id
        /// </summary>
        public int TrainId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// 考勤率-最低
        /// </summary>
        public double MinRate { get; set; }
        /// <summary>
        /// 考勤率-最高
        /// </summary>
        public double MaxRate { get; set; }

        /// <summary>
        /// 查询时间段
        /// </summary>
        public List<TimeList> TimeList { get; set; }
    }
}
