// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   集合类的扩展方法
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Collections.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        /// <typeparam name="T">
        /// 集合元素类型
        /// </typeparam>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count <= 0;
        }
    }
}