// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapToAttribute.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   AutoMapTo特性，标示当前类可以映射到目标类型
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.AutoMapper
{
    using System;

    /// <summary>
    /// AutoMapTo特性，标示当前类可以映射到目标类型
    /// </summary>
    public class AutoMapToAttribute : AutoMapAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetTypes">
        /// 目标类型
        /// </param>
        public AutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {
        }

        /// <summary>
        /// 映射方向
        /// </summary>
        internal override AutoMapDirection Direction
        {
            get
            {
                return AutoMapDirection.To;
            }
        }
    }
}