using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class TrainingCheckinDto
    {
        public int TrainingId { get; set; }
        public string CardNumber { get; set; }
        public System.DateTime CheckInTime { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
