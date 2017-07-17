using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class StudentResultDto
    {
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 学员卡号
        /// </summary>

        public string CardNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<StudentCheckInDto> list { get; set; }

        /// <summary>
        /// 考勤率
        /// </summary>
        public double Rate
        {
            get
            {
                if (this.list != null && this.list.Count > 0)
                {
                    var totalCount = 0;
                    var checkInCount = 0;
                    foreach (var item in list)
                    {
                        totalCount += item.list.Count;
                        checkInCount += item.CheckInNumber;
                    }
                    if (totalCount != 0)
                    {
                        return ((checkInCount * 1.0) / totalCount) * 100;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

    }

    public class StudentCheckInDto
    {
        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime TrainingDate { get; set; }

        /// <summary>
        /// 查询详情
        /// </summary>
        public List<StudentStaticDetail> list { get; set; }

        /// <summary>
        /// 签到的数量
        /// </summary>
        public int CheckInNumber { get { return list.Where(p => p.IsCheckIn == true).Count(); } }
    }

    public class StudentStaticDetail
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
        /// 是否签到
        /// </summary>
        public bool IsCheckIn { get; set; }

        public string IsCheckStr
        {
            get
            {
                if (this.IsCheckIn == true)
                {
                    return "已签到";
                }
                else
                {
                    return "未签到";
                }
            }
        }
    }
}
