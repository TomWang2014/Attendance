
namespace Attendance.Repository.Training
{
    using Attendance.Domain;
    using Attendance.Domain.Trainings;
    using Attendance.Infrastructure.Data;
    using Attendance.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TrainingTimesRepository : RepositoryBase<TrainingTimes>, ITrainingTimesRepository
    {
         /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public TrainingTimesRepository(AppContext context)
            : base(context)
        {
        }
    }
}
