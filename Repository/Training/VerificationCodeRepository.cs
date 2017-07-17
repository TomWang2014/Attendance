using Attendance.Infrastructure.Data;
using Attendance.Domain.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attendance.Domain;
using Attendance.Repository;

namespace Attendance.Repository.Training
{
    public class VerificationCodeRepository : RepositoryBase<TrainingVerificationCode>, IVerificationCodeRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public VerificationCodeRepository(AppContext context)
            : base(context)
        {
        }
    }
}
