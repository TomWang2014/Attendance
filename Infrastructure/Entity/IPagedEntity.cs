// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPagedEntity.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/7/17 13:38:02</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Entity
{
    /// <summary>
    /// 分页结果统一继承接口
    /// </summary>
    public interface IPagedEntity
    {
        /// <summary>
        /// 行索引
        /// </summary>
        int RowIndex { get; set; }

        /// <summary>
        /// 分页记录总行数
        /// </summary>
        int Totalcount { get; set; }
    }
}
