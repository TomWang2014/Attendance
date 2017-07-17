using Attendance.Domain;
using Attendance.Infrastructure.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    [AutoMap(typeof(TrainingTimes))]
    public class TrainingTimeDto
    {
        public System.DateTime TrainingDate { get; set; }
        public List<TrainingTimeDetails> Times { get; set; }

        public string TrainingDateStr
        {
            get
            {
                return this.TrainingDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
