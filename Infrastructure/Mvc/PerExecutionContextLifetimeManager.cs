// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerExecutionContextLifetimeManager.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   update@2015-5-5: 针对非http请求的情况使用了Runtime缓存
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Mvc
{
    using System;
    using System.Web;

    using Attendance.Infrastructure.Cache.Runtime;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// PerExecutionContextLifetimeManager
    /// </summary>
    public class PerExecutionContextLifetimeManager : LifetimeManager
    {
        /// <summary>
        /// contextCache
        /// </summary>
        private readonly RuntimeCache cache = new RuntimeCache();

        /// <summary>
        /// key
        /// </summary>
        private Guid key;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PerExecutionContextLifetimeManager()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">
        /// key
        /// </param>
        private PerExecutionContextLifetimeManager(Guid key)
        {
            if (key == Guid.Empty)
            {
                throw new ArgumentException();
            }

            this.key = key;
        }

        /// <summary>
        /// GetValue
        /// </summary>
        /// <returns>object</returns>
        public override object GetValue()
        {
            object result = null;

            if (HttpContext.Current != null)
            {
                var obj = HttpContext.Current.Items[this.key.ToString()];
                if (obj != null)
                {
                    result = obj;
                }
            }
            else
            {
                var obj = this.cache.Get<object>(this.key.ToString());
                if (obj != null)
                {
                    result = obj;
                }
            }

            return result;
        }

        /// <summary>
        /// RemoveValue
        /// </summary>
        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(this.key.ToString()))
                {
                    HttpContext.Current.Items.Remove(this.key.ToString());
                }
            }
            else
            {
                if (this.cache.Exists(this.key.ToString()))
                {
                    this.cache.Remove(this.key.ToString());
                }
            }
        }

        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="newValue">
        /// newValue
        /// </param>
        public override void SetValue(object newValue)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[this.key.ToString()] == null)
                {
                    HttpContext.Current.Items[this.key.ToString()] = newValue;
                }
            }
            else
            {
                this.cache.Add(this.key.ToString(), newValue);
            }
        }
    }
}
