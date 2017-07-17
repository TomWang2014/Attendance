// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapFromAttribute.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   AutoMapFrom特性，标示当前类可以从目标类型映射而来
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.AutoMapper
{
    using System;

    /// <summary>
    /// AutoMapFrom特性，标示当前类可以从目标类型映射而来
    /// </summary>
    public class AutoMapFromAttribute : AutoMapAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetTypes">
        /// 目标类型
        /// </param>
        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {
        }

        /// <summary>
        /// 映射方向
        /// </summary>
        internal override AutoMapDirection Direction
        {
            get { return AutoMapDirection.From; }
        }
    }
}