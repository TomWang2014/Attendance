
namespace Attendance.Application.SystemMgtServices.Dtos
{
    using Attendance.Domain;
    using Attendance.Infrastructure.AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AutoMap(typeof(TrainingVerificationCode))]
    public class Tab_VerificationCodeDto
    {
        public long Id { get; set; }
        public Nullable<int> StudentPhone { get; set; }
        public int TrainingId { get; set; }
        public Nullable<int> IsVerification { get; set; }
        public string VerificationCode { get; set; }
        public Nullable<System.DateTime> VerificationTime { get; set; }
    }
}
