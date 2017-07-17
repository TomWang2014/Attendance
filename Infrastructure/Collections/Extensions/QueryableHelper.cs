// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryableHelper.cs" company="zjzx">
//   2016-11-18 10:42:49
// </copyright>
// <summary>
//   The queryable helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Collections.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The queryable helper.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public static class QueryableHelper<T>
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private readonly static Dictionary<string, LambdaExpression> cache = new Dictionary<string, LambdaExpression>();

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="queryable">
        /// The queryable.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="desc">
        /// The desc.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> OrderBy(IQueryable<T> queryable, string propertyName, bool desc)
        {
            dynamic keySelector = GetLambdaExpression(propertyName);
            return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
        }

        /// <summary>
        /// The get lambda expression.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="LambdaExpression"/>.
        /// </returns>
        private static LambdaExpression GetLambdaExpression(string propertyName)
        {
            if (cache.ContainsKey(propertyName))
            {
                return cache[propertyName];
            }

            var param = Expression.Parameter(typeof(T));
            var body = Expression.Property(param, propertyName);
            var keySelector = Expression.Lambda(body, param);
            cache[propertyName] = keySelector;
            return keySelector;
        }
    }
}