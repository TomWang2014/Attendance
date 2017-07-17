// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryBase.cs" company="zjzx">
//   2016-11-8 18:11:05
// </copyright>
// <author>李天赐</author>
// <summary>
//   IRepository接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Attendance.Infrastructure.Entity;

    /// <summary>
    /// IRepository接口
    /// </summary>
    /// <typeparam name="TEntity">
    /// 实体类型
    /// </typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// 向仓储中添加一条对象
        /// </summary>
        /// <param name="entity">
        /// 要添加的对象
        /// </param>
        void Add(TEntity entity);

        /// <summary>
        /// 从删除中删除对象
        /// </summary>
        /// <param name="entity">
        /// 被删除的对象
        /// </param>
        void Remove(TEntity entity);

        /// <summary>
        /// 从删除中删除对象通过ID删除
        /// </summary>
        /// <param name="entity">
        /// 被删除的对象
        /// </param>
        void Delete(TEntity entity);

        /// <summary>
        /// 将对象的修改保存到仓储中
        /// </summary>
        /// <param name="entity">
        /// 被修改的对象
        /// </param>
        void Modify(TEntity entity);

        /// <summary>
        /// 通过Id获取一个对象
        /// </summary>
        /// <param name="id">
        /// 对象ID
        /// </param>
        /// <returns>
        /// 目标对象
        /// </returns>
        TEntity GetModel(object id);

        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <returns>目标对象列表</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// 获取所有子类型列表
        /// </summary>
        /// <typeparam name="TSEntity">子类的类型</typeparam>
        /// <returns>子类型列表</returns>
        IEnumerable<TSEntity> GetAllSubType<TSEntity>() where TSEntity : TEntity;

        /// <summary>
        /// 分页获取所有对象
        /// </summary>
        /// <typeparam name="TS">
        /// 对象类型
        /// </typeparam>
        /// <param name="pageIndex">
        /// 页索引
        /// </param>
        /// <param name="pageSize">
        /// 每页记录数
        /// </param>
        /// <param name="orderByExpression">
        /// 排序表达式
        /// </param>
        /// <param name="ascending">
        /// 指定是否升序
        /// </param>
        /// <returns>
        /// 查询得到的分页结果
        /// </returns>
        IEnumerable<TEntity> GetPagedElements<TS>(int pageIndex, int pageSize, Expression<Func<TEntity, TS>> orderByExpression, bool ascending);

        /// <summary>
        /// 分页获取子类型对象
        /// </summary>
        /// <typeparam name="TS">
        /// 排序字段类型
        /// </typeparam>
        /// <typeparam name="TSEntity">
        /// 子类型
        /// </typeparam>
        /// <param name="pageIndex">
        /// 页索引
        /// </param>
        /// <param name="pageSize">
        /// 每页记录数
        /// </param>
        /// <param name="orderByExpression">
        /// 排序表达式
        /// </param>
        /// <param name="ascending">
        /// 是否升序
        /// </param>
        /// <returns>
        /// 查询得到的分页结果
        /// </returns>
        IEnumerable<TSEntity> GetPagedSubTypeElements<TS, TSEntity>(int pageIndex, int pageSize, Expression<Func<TSEntity, TS>> orderByExpression, bool ascending) where TSEntity : TEntity;

        /// <summary>
        /// 条件查询（不根据租户id进行过滤，仅仅对租户特定数据有效，对非租户特定的数据等同于GetFilteredElements）
        /// </summary>
        /// <param name="filter">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// 条件查询的结果
        /// </returns>
        IEnumerable<TEntity> GetNoTenantFilteredElements(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <returns>
        /// 条件查询的结果
        /// </returns>
        IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="TS">
        /// 对象类型
        /// </typeparam>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <param name="pageIndex">
        /// 页索引
        /// </param>
        /// <param name="pageSize">
        /// 每页记录数
        /// </param>
        /// <param name="orderByExpression">
        /// 排序表达式
        /// </param>
        /// <param name="ascending">
        /// 指定是否升序
        /// </param>
        /// <returns>
        /// 条件查询的结果
        /// </returns>
        IEnumerable<TEntity> GetFilteredElements<TS>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TS>> orderByExpression, bool ascending);

        /// <summary>
        /// 通过条件获取分页数据
        /// </summary>
        /// <typeparam name="TS">
        /// 对象类型
        /// </typeparam>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <param name="pageIndex">
        /// 页索引
        /// </param>
        /// <param name="pageSize">
        /// 每页记录数
        /// </param>
        /// <param name="orderByExpression">
        /// 排序表达式
        /// </param>
        /// <param name="ascending">
        /// 指定是否升序
        /// </param>
        /// <returns>
        /// 分页结果
        /// </returns>
        PagedResult<TEntity> GetFilteredPageResult<TS>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TS>> orderByExpression, bool ascending);

        /// <summary>
        /// 通过条件获取分页数据
        /// </summary>
        /// <typeparam name="TS">
        /// 对象类型
        /// </typeparam>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <param name="pageIndex">
        /// 页索引
        /// </param>
        /// <param name="pageSize">
        /// 每页记录数
        /// </param>
        /// <param name="orderBy">
        /// 排序表达式
        /// </param>
        /// <param name="ascending">
        /// 指定是否升序
        /// </param>
        /// <returns>
        /// 分页结果
        /// </returns>
        PagedResult<TEntity> GetFilteredPageResult<TS>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, string orderBy, bool ascending);


        /// <summary>
        /// 通过条件获取子类型的分页数据
        /// </summary>
        /// <typeparam name="TS">
        /// 对象类型
        /// </typeparam>
        /// <typeparam name="TSEntity">
        /// 子类的类型
        /// </typeparam>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <param name="pageIndex">
        /// 页索引
        /// </param>
        /// <param name="pageSize">
        /// 每页记录数
        /// </param>
        /// <param name="orderByExpression">
        /// 排序表达式
        /// </param>
        /// <param name="ascending">
        /// 指定是否升序
        /// </param>
        /// <returns>
        /// 分页数据
        /// </returns>
        PagedResult<TSEntity> GetFilteredSubTypePageResult<TS, TSEntity>(Expression<Func<TSEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TSEntity, TS>> orderByExpression, bool ascending) where TSEntity : TEntity;

        /// <summary>
        /// 是否存在符合条件的对象
        /// </summary>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <returns>
        /// true:存在,flase:不存在
        /// </returns>
        bool IsExsist(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 检测是否存在符合条件的子类型对象
        /// </summary>
        /// <typeparam name="TSEntity">
        /// 子对象类型
        /// </typeparam>
        /// <param name="filter">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// 是否存在符合条件的子类型对象
        /// </returns>
        bool IsExsistSubType<TSEntity>(Expression<Func<TSEntity, bool>> filter) where TSEntity : TEntity;

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <param name="isTenanted">
        /// 是否是租户特定的
        /// </param>
        /// <returns>
        /// 符合条件的总记录数
        /// </returns>
        int GetRecordCount(Expression<Func<TEntity, bool>> filter, bool isTenanted = true);

        /// <summary>
        /// 获取子类型记录数
        /// </summary>
        /// <typeparam name="TSEntity">
        /// 子类的类型
        /// </typeparam>
        /// <param name="filter">
        /// 筛选条件
        /// </param>
        /// <returns>
        /// TEntity
        /// </returns>
        int GetSubTypeRecordCount<TSEntity>(Expression<Func<TSEntity, bool>> filter) where TSEntity : TEntity;

        /// <summary>
        /// 计算和
        /// </summary>
        /// <param name="filter">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// SUM值
        /// </returns>
        decimal Sum(Expression<Func<TEntity, decimal?>> filter);

        /// <summary>
        /// 统计总数
        /// </summary>
        /// <param name="filter">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// 总数
        /// </returns>
        int Count(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 统计子类型的总数
        /// </summary>
        /// <typeparam name="TSEntity">
        /// 子类型
        /// </typeparam>
        /// <param name="filter">
        /// 过滤条件
        /// </param>
        /// <returns>
        /// 子类型的总数
        /// </returns>
        int Count<TSEntity>(Expression<Func<TSEntity, bool>> filter) where TSEntity : TEntity;
    }
}
