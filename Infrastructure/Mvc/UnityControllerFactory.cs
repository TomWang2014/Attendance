// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityControllerFactory.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UnityControllerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// UnityControllerFactory
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
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
        public UnityControllerFactory(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// GetControllerInstance
        /// </summary>
        /// <param name="requestContext">
        /// requestContext
        /// </param>
        /// <param name="controllerType">
        /// controllerType
        /// </param>
        /// <returns>
        /// IController
        /// </returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                var val = null == controllerType ? null : (IController)this.container.Resolve(controllerType);
                return val;
            }
            catch
            {
                throw;
            }
        }
    }
}
