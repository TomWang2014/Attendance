// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="zjzx">
//   2016-11-21 13:47:14
// </copyright>
// <author>李天赐</author>
// <summary>
//   缓存相关操作的接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Cache
{
    using System;

    /// <summary>
    /// 缓存相关操作的接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 向Cache中添加缓存对象
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        void Add(string key, object obj);

        /// <summary>
        /// 向Cache中添加缓存对象，如果时间达到指定的秒数即被移除
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        /// <param name="seconds">
        /// 固定的超时秒数
        /// </param>
        void Add(string key, object obj, int seconds);

        /// <summary>
        /// 向Cache中添加缓存对象
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        /// <param name="slidingExpiration">
        /// 指定时间段
        /// </param>
        void Add(string key, object obj, TimeSpan slidingExpiration);

        /// <summary>
        /// 向Cache中添加缓存对象
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        /// <param name="expires">
        /// 过期日期和时间
        /// </param>
        void Add(string key, object obj, DateTime expires);

        /// <summary>
        /// 从Cache中判断指定的Key是否已经有缓存数据
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <returns>
        /// true/false
        /// </returns>
        bool Exists(string key);

        /// <summary>
        /// 从Cache中获取缓存的对象
        /// </summary>
        /// <typeparam name="T">
        /// 缓存对象的类型
        /// </typeparam>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <returns>
        /// 缓存的对象
        /// </returns>
        T Get<T>(string key);

        /// <summary>
        /// 向Cache中永久缓存对象
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        void Max(string key, object obj);

        /// <summary>
        /// 从Cache中移除缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        void Remove(string key);

        /// <summary>
        /// 测试Cache是否可用
        /// </summary>
        /// <returns>Cache是否可用</returns>
        bool Test();
    }
}
