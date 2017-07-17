// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="">
//   
// </copyright>
// <author>朱新亮</author>
// <summary>
//   UnitOfWork接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Data
{
    using System;

    /// <summary>
    /// UnitOfWork接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交
        /// </summary>
        void Commit();
    }
}
