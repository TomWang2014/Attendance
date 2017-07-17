// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHasModifyTime.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   标准化接口，要求有修改时间(CreationTime)
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Entity.Auditing
{
    using System;

    /// <summary>
    /// 标准化接口，要求有修改时间(CreationTime)
    /// An entity can implement this interface if <see cref="IHasModifyTime"/> of this entity must be stored.
    /// <see cref="IHasModifyTime"/> can be automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface IHasModifyTime
    {
        /// <summary>
        /// 创建时间
        /// Creation time of this entity.
        /// </summary>
        DateTime ModifyTime { get; set; }
    }
}