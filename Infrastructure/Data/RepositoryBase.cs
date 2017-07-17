// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="zjzx">
//   2016-11-18 10:44:31
// </copyright>
// <author>李天赐</author>
// <date>2016-11-18 10:44:41</date>
// <summary>
//   数据仓库基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    using Attendance.Infrastructure.Collections.Extensions;
    using Attendance.Infrastructure.Dto;
    using Attendance.Infrastructure.Entity;
    using Attendance.Infrastructure.Exceptions;

    /// <summary>
    /// 数据仓库基类
    /// </summary>
    /// <typeparam name="TEntity">
    /// 对象类型
    /// </typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// 实体集合
        /// </summary>
        private readonly IDbSet<TEntity> dataBaseSet;

        /// <summary>
        /// 利用构造函数设置数据库上下文和实体集合
        /// </summary>
        /// <param name="context">
        /// 数据库上下文
        /// </param>
        public RepositoryBase(DbContext context)
        {
            this.Context = context;
            this.dataBaseSet = this.DataContext.Set<TEntity>();
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// 实体集合
        /// </summary>
        protected IDbSet<TEntity> DbSet
        {
            get
            {
                return this.dataBaseSet;
            }
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected DbContext DataContext
        {
            get { return this.Context; }
        }

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            this.dataBaseSet.Add(entity);
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            this.dataBaseSet.Remove(entity);
        }

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            DbEntityEntry<TEntity> entry = this.DataContext.Entry<TEntity>(entity);

            // 2,将伪包装类对象的状态设置为unchanged
            entry.State = System.Data.Entity.EntityState.Deleted;
        }

        /// <inheritdoc/>
        public virtual void Modify(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }


            this.dataBaseSet.Attach(entity);
            this.DataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public TEntity GetModel(object id)
        {
            var entity = this.dataBaseSet.Find(id);

            if (entity == null)
            {
                throw new UserFriendlyException("该记录已不存在，请刷新后重试!");
            }

            return this.dataBaseSet.Find(id);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.dataBaseSet.AsEnumerable<TEntity>();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetPagedElements<TS>(int pageIndex, int pageSize, Expression<Func<TEntity, TS>> orderByExpression, bool ascending)
        {
            return ascending
                ?
                    this.dataBaseSet
                    .OrderBy(orderByExpression)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                :
                    this.dataBaseSet
                    .OrderByDescending(orderByExpression)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }


        /// <inheritdoc/>
        public IEnumerable<TEntity> GetNoTenantFilteredElements(Expression<Func<TEntity, bool>> filter)
        {
            return this.GetFilteredElements(filter);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", EnityFilterMessagesDto.ExceptionFilterCannotBeNull);
            }

            return this.dataBaseSet.Where(filter).AsEnumerable();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetFilteredElements<S>(
            Expression<Func<TEntity, bool>> filter,
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, S>> orderByExpression,
            bool ascending)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", EnityFilterMessagesDto.ExceptionFilterCannotBeNull);
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(EnityFilterMessagesDto.ExceptionInvalidPageIndex);
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException(EnityFilterMessagesDto.ExceptionInvalidPageSize);
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException(
                    "orderByExpression",
                    EnityFilterMessagesDto.ExceptionOrderByExpressionCannotBeNull);
            }

            return ascending
                       ? this.dataBaseSet.Where(filter)
                             .OrderBy(orderByExpression)
                             .Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize)
                             .ToList()
                       : this.dataBaseSet.Where(filter)
                             .OrderByDescending(orderByExpression)
                             .Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
        }

        /// <inheritdoc/>
        public PagedResult<TEntity> GetFilteredPageResult<S>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, S>> orderByExpression, bool ascending)
        {
            var count = this.dataBaseSet.Where(filter).Count();

            var pageCount = (int)Math.Ceiling((double)count / pageSize);

            if (pageIndex > 0 && pageCount != 0)
            {
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            }

            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            return new PagedResult<TEntity>(
                pageIndex,
                pageSize,
                count,
                this.GetFilteredElements(filter, pageIndex, pageSize, orderByExpression, ascending));
        }


        public PagedResult<TEntity> GetFilteredPageResult<TS>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, string orderBy, bool @ascending)
        {
            var count = this.dataBaseSet.Where(filter).Count();

            var pageCount = (int)Math.Ceiling((double)count / pageSize);

            if (pageIndex > 0 && pageCount != 0)
            {
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            }

            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            return new PagedResult<TEntity>(
                pageIndex,
                pageSize,
                count,
                this.GetFilteredElements<TEntity>(filter, pageIndex, pageSize, orderBy, ascending));

        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetFilteredElements<S>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, string orderBy, bool ascending)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", EnityFilterMessagesDto.ExceptionFilterCannotBeNull);
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(EnityFilterMessagesDto.ExceptionInvalidPageIndex);
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException(EnityFilterMessagesDto.ExceptionInvalidPageSize);
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                throw new ArgumentNullException("orderBy", EnityFilterMessagesDto.ExceptionFilterCannotBeNull);
            }

            var query = this.dataBaseSet.Where(filter);
            return query.OrderBy(orderBy, ascending).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// The get filtered sub type page result.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="orderByExpression">
        /// The order by expression.
        /// </param>
        /// <param name="ascending">
        /// The ascending.
        /// </param>
        /// <typeparam name="TS">
        /// </typeparam>
        /// <typeparam name="TSEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="PagedResult"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public PagedResult<TSEntity> GetFilteredSubTypePageResult<TS, TSEntity>(
            Expression<Func<TSEntity, bool>> filter,
            int pageIndex,
            int pageSize,
            Expression<Func<TSEntity, TS>> orderByExpression,
            bool @ascending) where TSEntity : TEntity
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<SEntity> GetAllSubType<SEntity>() where SEntity : TEntity
        {
            return this.dataBaseSet.OfType<SEntity>().AsEnumerable();
        }

        /// <inheritdoc/>
        public IEnumerable<SEntity> GetPagedSubTypeElements<S, SEntity>(int pageIndex, int pageSize, Expression<Func<SEntity, S>> orderByExpression, bool ascending) where SEntity : TEntity
        {
            return ascending
                ?
                    this.dataBaseSet.OfType<SEntity>()
                    .OrderBy(orderByExpression)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                :
                    this.dataBaseSet.OfType<SEntity>()
                    .OrderByDescending(orderByExpression)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<SEntity> GetFilteredSubTypeElements<SEntity>(Expression<Func<SEntity, bool>> filter) where SEntity : TEntity
        {
            return this.dataBaseSet.OfType<SEntity>().Where(filter).AsEnumerable();
        }




        /// <inheritdoc/>
        public bool IsExsist(Expression<Func<TEntity, bool>> filter)
        {
            return this.GetRecordCount(filter) > 0;
        }

        /// <inheritdoc/>
        public bool IsExsistSubType<TSEntity>(Expression<Func<TSEntity, bool>> filter) where TSEntity : TEntity
        {
            return this.GetSubTypeRecordCount(filter) > 0;
        }

        /// <inheritdoc/>
        public int GetRecordCount(Expression<Func<TEntity, bool>> filter, bool isTenanted = true)
        {
            return this.dataBaseSet.Where(filter).Count();
        }

        // public int GetRecordCount(Expression<Func<TEntity, bool>> filter)
        // {
        // return _dbSet.Where(filter).Count();
        // }

        /// <inheritdoc/>
        public int GetSubTypeRecordCount<TSEntity>(Expression<Func<TSEntity, bool>> filter) where TSEntity : TEntity
        {
            return this.dataBaseSet.OfType<TSEntity>().Where(filter).Count();
        }

        /// <inheritdoc/>
        public decimal Sum(Expression<Func<TEntity, decimal?>> filter)
        {
            return this.dataBaseSet.OfType<TEntity>().Sum(filter) ?? 0;
        }

        /// <inheritdoc/>
        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return this.dataBaseSet.OfType<TEntity>().Count(filter);
        }

        /// <inheritdoc/>
        public int Count<TSEntity>(Expression<Func<TSEntity, bool>> filter) where TSEntity : TEntity
        {
            return this.dataBaseSet.OfType<TSEntity>().Count(filter);
        }

        /// <summary>
        /// The get filtered elements without.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TEntity> GetFilteredElementsWithout(
            Expression<Func<TEntity, bool>> filter)
        {
            return this.dataBaseSet.Where(filter);
        }
    }
}
