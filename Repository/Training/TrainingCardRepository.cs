
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
    public class TrainingCardRepository : RepositoryBase<TrainingCard>, ITrainingCardRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public TrainingCardRepository(AppContext context)
            : base(context)
        {
        }

    }
}
