// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapAttribute.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/3/17 11:19:05</date>
// <summary>
//   自定义AutoMap特性，用来作为自动注册的标示。
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.AutoMapper
{
    using System;

    /// <summary>
    /// 自定义AutoMap特性
    /// </summary>
    public class AutoMapAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetTypes">
        /// 目标类型
        /// </param>
        public AutoMapAttribute(params Type[] targetTypes)
        {
            this.TargetTypes = targetTypes;
        }

        /// <summary>
        /// 目标类型，只读。
        /// </summary>
        public Type[] TargetTypes { get; private set; } 

        /// <summary>
        /// AutoMap的方向
        /// </summary>
        internal virtual AutoMapDirection Direction
        {
            get { return AutoMapDirection.From | AutoMapDirection.To; }
        }
    }
}