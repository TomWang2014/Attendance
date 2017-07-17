// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultAssemblyFinder.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/6/17 13:45:14</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// DefaultAssemblyFinder
    /// If gets assemblies from current domain.
    /// </summary>
    public class DefaultAssemblyFinder
    {
        /// <summary>
        /// Gets Singleton instance of <see cref="DefaultAssemblyFinder"/>.
        /// </summary>
        public static DefaultAssemblyFinder Instance { get { return SingletonInstance; } }

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static readonly DefaultAssemblyFinder SingletonInstance = new DefaultAssemblyFinder();

        /// <summary>
        /// Private constructor to disable instancing.
        /// </summary>
        private DefaultAssemblyFinder()
        {

        }

        /// <summary>
        /// The get all assemblies.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}