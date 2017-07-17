// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapExtensions.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   AutoMap的扩展方法
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.AutoMapper
{
    using global::AutoMapper;

    /// <summary>
    ///  AutoMap的扩展方法
    /// </summary>
    public static class AutoMapExtensions
    {
        /// <summary>
        /// 使用AutoMapper类库将一个对象转换为另一个对象。创建一个新的<see cref="TDestination"/>的对象
        /// 在调用该方法之前，必须要有两个对象的映射关系。
        /// </summary>
        /// <typeparam name="TDestination">
        /// 目标对象的类型
        /// </typeparam>
        /// <param name="source">
        /// 源对象
        /// </param>
        /// <returns>
        /// 目标对象<see cref="TDestination"/>.
        /// </returns>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 从源对象到目标对象执行映射。
        /// 在调用该方法之前，必须要有两个对象的映射关系。
        /// </summary>
        /// <typeparam name="TSource">
        /// 源对象类型
        /// </typeparam>
        /// <typeparam name="TDestination">
        /// 目标对象类型
        /// </typeparam>
        /// <param name="source">
        /// 源对象
        /// </param>
        /// <param name="destination">
        /// 目标对象
        /// </param>
        /// <returns>
        /// 目标对象<see cref="TDestination"/>.
        /// </returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
