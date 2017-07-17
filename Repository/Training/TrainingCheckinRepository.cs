using Attendance.Domain;
using Attendance.Domain.Trainings;
using Attendance.Infrastructure.Data;
using Attendance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Repository.Training
{
    public class TrainingCheckinRepository : RepositoryBase<TrainingCheckin>, ITrainingCheckinRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public TrainingCheckinRepository(AppContext context)
            : base(context)
        {
        }

    }
}
