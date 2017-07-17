// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtension.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/18 9:51:05</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Collections.Extensions
{
    using System.Linq;

    /// <summary>
    /// QueryableExtensions
    /// </summary>
    public static class QueryableExtension
    {
        /// <summary>
        /// 根据字段名称排序扩展
        /// </summary>
        /// <param name="queryable"> The queryable. </param>
        /// <param name="propertyName">排序字段 </param>
        /// <typeparam name="T">T</typeparam>
        /// <returns> 返回IQueryable </returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName)
        {
            return QueryableHelper<T>.OrderBy(queryable, propertyName, false);
        }

        /// <summary>
        /// 根据字段名称排序扩展
        /// </summary>
        /// <param name="queryable"> The queryable. </param>
        /// <param name="propertyName">排序字段 </param>
        /// <param name="desc">是否升序</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns> 返回IQueryable </returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, bool desc)
        {
            return QueryableHelper<T>.OrderBy(queryable, propertyName, desc);
        }
    }
}

