// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodRoleAttribute.cs" company="">
//   
// </copyright>
// <author>li.zhao</author>
// <date>2015/5/18 10:53:03</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Unity.Aop
{
    using System;

    /// <summary>
    /// MethodRoleAttribute
    /// </summary>
    public class MethodRoleAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodRoleAttribute"/> class.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        public MethodRoleAttribute(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }
    }
}
