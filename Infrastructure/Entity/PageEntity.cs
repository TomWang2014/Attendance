// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PageEntity.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/7/11 11:16:07</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Entity
{
    /// <summary>
    /// 分页搜索条件
    /// </summary>
    public class PageEntity
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}
