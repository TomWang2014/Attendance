using Attendance.Infrastructure.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attendance.Domain;
namespace Attendance.Application.AttendanceMgtServices.Dtos
{

    [AutoMap(typeof(TrainingSignUp))]
    public class StudentListItem
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int CardId { get; set; }
        public string StudentName { get; set; }
        public string StudentPhone { get; set; }
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 培训班名称
        /// </summary>
        public string TrainingName { get; set; }

        /// <summary>
        /// 学员卡号
        /// </summary>
        public string TrainingCardCardNumber { get; set; }

        /// <summary>
        /// 报名时间
        /// </summary>
        public string CreateTimeStr
        {
            get
            {
                return CreateTime.ToString("yyyy年MM月dd日");
            }
        }

    }
}
