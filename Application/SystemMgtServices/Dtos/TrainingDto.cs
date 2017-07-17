
namespace Attendance.Application.SystemMgtServices.Dtos
{
    using Attendance.Domain;
    using Attendance.Infrastructure.AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [AutoMap(typeof(Training))]
    public class TrainingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string Descriptions { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> SignUpStart { get; set; }
        public Nullable<System.DateTime> SignUpEnd { get; set; }

        public string StartTimeStr
        {
            get
            {
                return this.StartTime.ToString("MM月dd日");
            }
        }

        public string EndTimeStr
        {
            get
            {
                return this.EndTime.ToString("MM月dd日");
            }
        }

        public string SignUpStartStr
        {
            get
            {
                return Convert.ToDateTime(this.SignUpStart).ToString("yyyy年MM月dd日");
            }
        }

        public string SignUpEndStr
        {
            get
            {
                return Convert.ToDateTime(this.SignUpEnd).ToString("yyyy年MM月dd日");
            }
        }
    }
}
