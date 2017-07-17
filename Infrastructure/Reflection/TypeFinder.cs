// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeFinder.cs" company="">
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
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Attendance.Infrastructure.Collections.Extensions;

    /// <summary>
    /// The type finder.
    /// </summary>
    public class TypeFinder
    {
        /// <summary>
        /// Gets or sets the assembly finder.
        /// </summary>
        public DefaultAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFinder"/> class.
        /// </summary>
        public TypeFinder()
        {
            this.AssemblyFinder = DefaultAssemblyFinder.Instance;
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="Type[]"/>.
        /// </returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return this.GetAllTypes().Where(predicate).ToArray();
        }

        /// <summary>
        /// The find all.
        /// </summary>
        /// <returns>
        /// The <see cref="Type[]"/>.
        /// </returns>
        public Type[] FindAll()
        {
            return this.GetAllTypes().ToArray();
        }

        /// <summary>
        /// The get all types.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<Type> GetAllTypes()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in this.AssemblyFinder.GetAllAssemblies().Distinct())
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    // TODO: handle exception
                    Debug.WriteLine(ex.Message);
                }
            }

            return allTypes;
        }
    }
}