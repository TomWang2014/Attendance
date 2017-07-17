namespace Attendance.Infrastructure.Unity.Ioc
{
    using System;
    using System.Configuration;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;
    /// <summary>
    /// Resolver
    /// </summary>
    public class IocManager : IDisposable
    {
        /// <summary> 
        /// Ioc容器管理
        /// </summary>
        public static IocManager Instance = new IocManager();

        /// <summary>
        /// 定义容器管理接口
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// 私有构造函数初始化
        /// </summary>
        private IocManager()
        {
            this.container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(this.container, "real");
        }

        /// <summary>
        /// 返回但前实例
        /// </summary>
        /// <returns>IUnityContainer</returns>
        public IUnityContainer GetContainer()
        {
            return this.container;
        }

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>返回该对象的类型</returns>
        public T Resolve<T>()
        {
            return this.container.Resolve<T>();
        }

        /// <summary>
        /// 检查给定的类型是否被注册
        /// </summary>
        /// <param name="type">
        /// 类型
        /// </param>
        /// <returns>
        /// 是否被注册
        /// </returns>
        public bool IsRegistered(Type type)
        {
            return this.container.IsRegistered(type);
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (this.container != null)
            {
                this.container.Dispose();
            }
        }
    }
}