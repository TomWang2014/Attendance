// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuntimeCache.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/3/17 10:57:47</date>
// <summary>
//   主要功能有：
//   基于Runtime缓存的实现
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Cache.Runtime
{
    using System;
    using System.Runtime.Caching;

    using Attendance.Infrastructure.Cache;

    /// <summary>
    /// 基于Runtime缓存的实现
    /// </summary>
    public class RuntimeCache : ICache
    {
        /// <summary>
        /// MemoryCache
        /// </summary>
        private readonly MemoryCache cache;

        /// <summary>
        /// CacheItemPolicy
        /// </summary>
        private readonly CacheItemPolicy defaultCacheItemPolicy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RuntimeCache()
        {
            this.cache = MemoryCache.Default;
            this.defaultCacheItemPolicy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromSeconds(60 * 2) };
        }

        /// <summary>
        /// 向Cache中添加缓存对象
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        public void Add(string key, object obj)
        {
            var cacheItem = new CacheItem(key, obj);
            this.cache.Set(cacheItem, this.defaultCacheItemPolicy);
        }

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
        public void Add(string key, object obj, int seconds)
        {
            this.cache.Set(key, obj, DateTimeOffset.Now.AddSeconds(seconds));
        }

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
        public void Add(string key, object obj, TimeSpan slidingExpiration)
        {
            var cacheItem = new CacheItem(key, obj);
            var cacheItemPolicy = new CacheItemPolicy { SlidingExpiration = slidingExpiration };
            this.cache.Set(cacheItem, cacheItemPolicy);
        }

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
        public void Add(string key, object obj, DateTime expires)
        {
            var cacheItem = new CacheItem(key, obj);
            var cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = expires };
            this.cache.Set(cacheItem, cacheItemPolicy);
        }

        /// <summary>
        /// 从Cache中判断指定的Key是否已经有缓存数据
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <returns>
        /// true/false
        /// </returns>
        public bool Exists(string key)
        {
            return this.cache.Get(key) != null;
        }

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
        public T Get<T>(string key)
        {
            return (T)this.cache.Get(key);
        }

        /// <summary>
        /// 向Cache中永久缓存对象
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        /// <param name="obj">
        /// 要缓存的对象
        /// </param>
        public void Max(string key, object obj)
        {
            var cacheItem = new CacheItem(key, obj);
            var cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.MaxValue.AddYears(-1), Priority = CacheItemPriority.NotRemovable };
            this.cache.Set(cacheItem, cacheItemPolicy);
        }

        /// <summary>
        /// 从Cache中移除缓存项
        /// </summary>
        /// <param name="key">
        /// 缓存Key
        /// </param>
        public void Remove(string key)
        {
            this.cache.Remove(key);
        }

        /// <summary>
        /// 测试Cache是否可用
        /// </summary>
        /// <returns>Cache是否可用</returns>
        public bool Test()
        {
            const string Key = "_##**Test**##_";
            const string Obj = "Test";
            this.Add(Key, Obj);
            var result = this.Get<string>(Key);
            return result == Obj;
        }
    }
}
