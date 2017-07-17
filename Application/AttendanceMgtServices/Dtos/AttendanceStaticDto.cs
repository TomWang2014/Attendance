using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class AttendanceStaticDto
    {
        public int TrainId { get; set; }
        public List<TimeList> TimeList { get; set; }
    }
    public class TimeList
    {
        public string date { get; set; }
        public List<TrainingDateDto> list { get; set; }
    }
}
