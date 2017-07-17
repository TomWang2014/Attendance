using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class TrainingSignUpItem
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrainingCardCardNumber { get; set; }
        public string StudentPhone { get; set; }
        public string StudentName { get; set; }

        public int TrainingId { get; set; }
    }
}
