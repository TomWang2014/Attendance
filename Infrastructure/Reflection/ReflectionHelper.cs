// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines helper methods for reflection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines helper methods for reflection.
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// Checks whether <paramref name="givenType"/> implements/inherits <paramref name="genericType"/>.
        /// </summary>
        /// <param name="givenType">
        /// Type to check
        /// </param>
        /// <param name="genericType">
        /// Generic type
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        /// <summary>
        /// Gets a list of attributes defined for a class member and it's declaring type including inherited attributes.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// Type of the attribute
        /// </typeparam>
        /// <param name="memberInfo">
        /// MemberInfo
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo) 
            where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            // Add attributes on the member
            if (memberInfo.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            // Add attributes on the class
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            return attributeList;
        }
    }
}