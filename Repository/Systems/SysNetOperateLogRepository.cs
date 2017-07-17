// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SysNetOperateLogRepository.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/8 16:15:31</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Repository.Systems
{

    using Attendance.Domain;
    using Attendance.Domain.Systems;
    using Attendance.Infrastructure.Data;
    using Attendance.Infrastructure.Entity;
    /// <summary>
    /// 用户管理仓储
    /// </summary>
    public class SysNetOperateLogRepository : RepositoryBase<NetOperateLog>, ISysNetOperateLogRepository
    {
        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public SysNetOperateLogRepository(AppContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 新增登录日志
        /// </summary>
        /// <param name="model">模型</param>
        public void AddLoginLog(LoginLogDto model)
        {
            var log = new NetOperateLog
            {
                Name = "登录日志",
                OperatorCreateTime = model.LoginTime,
                OperatorId = model.UserId,
                Field1 = model.AddressId,
                Field2 = model.BrowserInfo
            };

            this.Add(log);
            this.DataContext.SaveChanges();
        }

        /// <summary>
        /// 新增支付日志
        /// </summary>
        /// <param name="payDto">支付dto</param>
        public void AddPayLog(object payDto)
        {
        }
    }
}
