
namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    using Attendance.Domain;
    using Attendance.Infrastructure.AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    [AutoMap(typeof(Training))]
    public class TrainListItem
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
                return this.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public string EndTimeStr
        {
            get
            {
                return this.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public string SignUpStartStr
        {
            get
            {
                if (this.SignUpStart.HasValue)
                {
                    return Convert.ToDateTime(this.SignUpStart).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string SignUpEndStr
        {
            get
            {
                if (this.SignUpEnd.HasValue)
                {
                    return Convert.ToDateTime(this.SignUpEnd).ToString("yyyy-MM-dd HH:mm:ss");                    
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string CreateTimeStr
        {
            get
            {
                return this.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        /// <summary>
        /// 报名状态
        /// </summary>
        public string SignUpStatusStr
        {
            get
            {
                if (DateTime.Now < this.SignUpStart)
                {
                    return "报名未开始";
                }
                else if (DateTime.Now > this.SignUpStart && DateTime.Now < this.SignUpEnd)
                {
                    return "报名中";
                }
                else
                {
                    return "报名已结束";
                }
            }
        }
        /// <summary>
        /// 开课状态
        /// </summary>
        public string TrainingStatusStr
        {
            get
            {
                if (DateTime.Now < this.StartTime)
                {
                    return "未开课";
                }
                else if (DateTime.Now > this.StartTime && DateTime.Now < this.EndTime)
                {
                    return "授课中";
                }
                else
                {
                    return "已结课";
                }
            }
        }
    }
}
