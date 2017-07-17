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
    public class TrainingTimeDetails
    {
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }

    }
}
