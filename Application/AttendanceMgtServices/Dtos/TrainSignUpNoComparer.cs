using Attendance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceMgtServices.Dtos
{
    public class TrainSignUpNoComparer : IEqualityComparer<TrainingSignUp>
    {
        public bool Equals(TrainingSignUp p1, TrainingSignUp p2)
        {
            if (p1 == null)
                return p2 == null;
            return p1.CardId == p2.CardId;
        }

        public int GetHashCode(TrainingSignUp p)
        {
            if (p == null)
                return 0;
            return p.CardId.GetHashCode();
        }
    }
}
