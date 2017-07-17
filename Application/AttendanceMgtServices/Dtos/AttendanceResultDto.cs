using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class AttendanceResultDto
    {
        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime TrainingDate { get; set; }
        /// <summary>
        /// 查询详情
        /// </summary>
        public List<StaticDetail> list { get; set; }

    }

    public class StaticDetail
    {
        /// <summary>
        /// 开始
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 应到人数
        /// </summary>
        public int DueNumber { get; set; }
        /// <summary>
        /// 实到人数
        /// </summary>
        public int AttendanceNumber { get; set; }
        /// <summary>
        /// 缺席人数
        /// </summary>
        public int MissingNumber
        {
            get
            {
                return this.DueNumber - AttendanceNumber;
            }
        }
        /// <summary>
        /// 实到人数比例
        /// </summary>
        public double AttendancePro
        {
            get
            {
                return ((AttendanceNumber * 1.0) / DueNumber) * 100;
            }
        }
        /// <summary>
        /// 缺席人数比例
        /// </summary>
        public double MissingPro
        {
            get
            {
                return ((MissingNumber * 1.0) / DueNumber) * 100;
            }
        }
    }
}
