
namespace Attendance.Repository.Training
{
    using Attendance.Infrastructure.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Attendance.Domain.Trainings;
    using Attendance.Repository;
    using Attendance.Domain;
    public class TrainingSignUpRepository : RepositoryBase<TrainingSignUp>, ITrainingSignUpRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public TrainingSignUpRepository(AppContext context)
            : base(context)
        {
        }
    }
}
