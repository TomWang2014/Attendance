// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SysLogServices.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/12/9 11:04:45</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Application.SystemMgtServices
{

    using Attendance.Domain.Systems;
    using Attendance.Infrastructure.Data;
    using Attendance.Infrastructure.Entity;

    /// <summary>
    /// 数据库日志系统服务
    /// </summary>
    public class SysLogServices
    {
        /// <summary>
        /// 日志仓储
        /// </summary>
        private readonly ISysNetOperateLogRepository sysLogRepository;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logRepository">日志仓储</param>
        /// <param name="unitOfWork">工作单元</param>
        public SysLogServices(ISysNetOperateLogRepository logRepository, IUnitOfWork unitOfWork)
        {
            this.sysLogRepository = logRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 新增登录日志
        /// </summary>
        /// <param name="model">模型</param>
        public void AddLoginLog(LoginLogDto model)
        {
            this.sysLogRepository.AddLoginLog(model);
        }
    }
}
