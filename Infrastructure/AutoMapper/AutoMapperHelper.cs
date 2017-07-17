// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperHelper.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   AutoMapper帮助类，主要用来创建映射
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.AutoMapper
{
    using System;
    using System.Reflection;

    using global::AutoMapper;

    using Attendance.Infrastructure.Collections.Extensions;

    /// <summary>
    /// AutoMapper帮助类，主要用来创建映射
    /// </summary>
    internal static class AutoMapperHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">
        /// 目标类型
        /// </param>
        public static void CreateMap(Type type)
        {
            CreateMap<AutoMapFromAttribute>(type);
            CreateMap<AutoMapToAttribute>(type);
            CreateMap<AutoMapAttribute>(type);
        }

        /// <summary>
        /// 创建映射
        /// </summary>
        /// <typeparam name="TAttribute">
        /// AutoMapAttribute类型
        /// </typeparam>
        /// <param name="type">
        /// AutoMapAttribute
        /// </param>
        public static void CreateMap<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapToAttribute.TargetTypes.IsNullOrEmpty())
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {

                        Mapper.CreateMap(type, targetType);
                    }

                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {
                        Mapper.CreateMap(targetType, type);
                    }
                }
            }
        }
    }
}