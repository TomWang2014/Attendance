using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Attendance.Domain.Systems
{
    using Attendance.Infrastructure.Data;
    using Attendance.Infrastructure.Entity;

    /// <summary>
    /// 日志服务接口
    /// </summary>
    public interface ISysNetOperateLogRepository : IRepositoryBase<NetOperateLog>
    {
        /// <summary>
        /// 新增登录日志
        /// </summary>
        /// <param name="model">登录模型</param>
        void AddLoginLog(LoginLogDto model);

        /// <summary>
        /// 新增支付日志
        /// </summary>
        /// <param name="payDto">支付字段Dto</param>
        void AddPayLog(object payDto);
    }
}
