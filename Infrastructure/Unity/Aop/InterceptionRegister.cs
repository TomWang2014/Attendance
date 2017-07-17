// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterceptionRegister.cs" company="zjzx">
//   2016-11-9 13:19:41
// </copyright>
// <author>李天赐</author>
// <date>2015/4/1 17:37:25</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Unity.Aop
{
    using System;

    using Attendance.Infrastructure.Reflection;
    using Attendance.Infrastructure.Unity.Ioc;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// InterceptionRegister
    /// </summary>
    public class InterceptionRegister
    {
        /// <summary>
        /// The _type finder.
        /// </summary>
        private readonly TypeFinder typeFinder;

        /// <summary>
        /// The _created mappings before.
        /// </summary>
        private static bool createdMappingsBefore;

        /// <summary>
        /// The sync obj.
        /// </summary>
        private static readonly object SyncObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptionRegister"/> class.
        /// </summary>
        public InterceptionRegister()
        {
            this.typeFinder = new TypeFinder();
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public void Initialize()
        {
            lock (SyncObj)
            {
                // We should prevent duplicate initialize in an application,
                if (createdMappingsBefore)
                {
                    return;
                }

                this.FindAndRegister();

                createdMappingsBefore = true;
            }
        }

        /// <summary>
        /// The find and register.
        /// </summary>
        private void FindAndRegister()
        {
            var types = this.typeFinder.Find(type => this.FindType(type));

            IocManager.Instance.GetContainer().AddNewExtension<Interception>();

            foreach (var foundType in types)
            {
                IocManager.Instance.GetContainer()
                    .RegisterType(
                        foundType,
                        new Interceptor<TransparentProxyInterceptor>(),
                        new InterceptionBehavior<ValidationInterceptionBehavior>());
            }
        }

        /// <summary>
        /// The find type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool FindType(Type type)
        {
            var result = false;
            if (type != null)
            {
                if (type.BaseType == typeof(InterceptiveObject))
                {
                    result = true;
                }
                else
                {
                    return this.FindType(type.BaseType);
                }
            }

            return result;
        }
    }
}
