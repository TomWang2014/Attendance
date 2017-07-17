// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityControllerApiFactory.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UnityControllerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// UnityControllerFactory
    /// </summary>
    public class UnityControllerApiFactory : System.Web.Http.Dependencies.IDependencyResolver
    {
        /// <summary>
        /// ioc容器
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container">
        /// ioc容器
        /// </param>
        public UnityControllerApiFactory(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// The get services.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <returns>
        /// The <see cref="IDependencyScope"/>.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityControllerApiFactory(child);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }
    }
}
