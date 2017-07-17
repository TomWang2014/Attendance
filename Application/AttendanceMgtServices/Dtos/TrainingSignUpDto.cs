using Attendance.Domain;
using Attendance.Infrastructure.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    [AutoMap(typeof(TrainingSignUp))]
    public class TrainingSignUpDto
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int CardId { get; set; }
        public string StudentName { get; set; }
        public string StudentPhone { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
