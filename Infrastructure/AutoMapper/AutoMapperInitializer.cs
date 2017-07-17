// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperInitializer.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   AutoMapper初始化器，用来自动创建映射关系
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.AutoMapper
{
    using System;
    using System.Collections;
    using System.Reflection;

    using global::AutoMapper;

    using Attendance.Infrastructure.Entity;
    using Attendance.Infrastructure.Reflection;

    /// <summary>
    ///  AutoMapper初始化器，用来自动创建映射关系
    /// </summary>
    public class AutoMapperInitializer
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object SyncObj = new object();

        /// <summary>
        /// 是否已经创建过映射
        /// </summary>
        private static bool createdMappingsBefore;

        /// <summary>
        /// TypeFinder, 类型查找器
        /// </summary>
        private readonly TypeFinder typeFinder;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoMapperInitializer()
        {
            this.typeFinder = new TypeFinder();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="typeFinder">
        /// 类型查找器
        /// </param>
        public AutoMapperInitializer(TypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public void Initialize()
        {
            this.CreateMappings();

            this.CreateOtherMappings();
        }

        /// <summary>
        /// 创建映射
        /// </summary>
        private void CreateMappings()
        {
            lock (SyncObj)
            {
                // We should prevent duplicate mapping in an application,
                // since AutoMapper is static.
                if (createdMappingsBefore)
                {
                    return;
                }

                this.FindAndAutoMapTypes();

                createdMappingsBefore = true;
            }
        }

        /// <summary>
        /// 查找并且构造映射
        /// </summary>
        private void FindAndAutoMapTypes()
        {
            var types = this.typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapAttribute)) ||
                type.IsDefined(typeof(AutoMapFromAttribute)) ||
                type.IsDefined(typeof(AutoMapToAttribute)));

            foreach (var type in types)
            {
                AutoMapperHelper.CreateMap(type);
            }
        }

        /// <summary>
        /// 构造其他特殊的映射
        /// </summary>
        private void CreateOtherMappings()
        {
            Mapper.CreateMap<IList, int>().ConvertUsing(list => list.Count);
            Mapper.CreateMap<DateTime, string>().ConvertUsing(d => default(DateTime) == d ? string.Empty : d.ToString("yyyy-MM-dd"));

            Mapper.CreateMap<DateTime, DateTimeFormatDto>().ConvertUsing(p =>
                 default(DateTime) == p ? null :
                 new DateTimeFormatDto
                 {
                     I = p.ToShortDateString(), 
                     M = p.ToShortTimeString(), 
                     S = p.ToString("yyyy-MM-dd HH:mm")
                 });

            Mapper.CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}
