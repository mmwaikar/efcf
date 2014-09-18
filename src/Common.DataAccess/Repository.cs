using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Entities.Common;

namespace Common.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            var efUnitOfWork = unitOfWork as UnitOfWork;
            if (efUnitOfWork == null) throw new Exception("Must be EFUnitOfWork"); // TODO: Typed exception
            _dbSet = efUnitOfWork.GetDbSet<TEntity>();
        }

        public IEnumerable<TEntity> All
        {
            get { return _dbSet; }
        }

        public IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(_dbSet, (current, includeProperty) => current.Include(includeProperty));
        }

        public TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.FirstOrDefault(@where);
        }

        public TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return AllIncluding(includeProperties).FirstOrDefault(@where.Compile());
        }

        public IEnumerable<TEntity> FindMany(Func<TEntity, bool> where)
        {
            return _dbSet.Where(@where).ToList();
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = _dbSet.Where(@where);
            entities = includeProperties.Aggregate(entities, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy == null)
            {
                return entities.ToList();
            }

            return entities.OrderBy(orderBy).ToList();
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy, int page, int pageSize, out int rows)
        {
            page = page - 1;
            rows = _dbSet.Count(@where);

            return _dbSet.Where(@where)
                         .OrderBy(orderBy)
                         .Skip(page*pageSize)
                         .Take(pageSize)
                         .ToList();
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy, int page, int pageSize, out int rows,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            page = page - 1;
            rows = _dbSet.Count(@where);

            return AllIncluding(includeProperties)
                .Where(@where.Compile())
                .OrderBy(orderBy)
                .Skip(page*pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> @where, Func<TEntity, object> orderBy, string sortOrder, int page, int pageSize, out int rows,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            page = page - 1;
            rows = _dbSet.Count(@where);

            if (sortOrder.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
            {
                return AllIncluding(includeProperties)
                .Where(@where.Compile())
                .OrderBy(orderBy)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            }

            return AllIncluding(includeProperties)
                .Where(@where.Compile())
                .OrderByDescending(orderBy)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Find( int id , params Expression<Func<TEntity , object>>[] includeProperties )
        {
            return AllIncluding(includeProperties).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> FindMany( int id , params Expression<Func<TEntity , object>>[] includeProperties )
        {
            return AllIncluding(includeProperties).Where(e => e.Id == id); //.FirstOrDefault(e => e.Id == id);
        }

        public void InsertOrUpdate(TEntity T)
        {
            _dbSet.AddOrUpdate(T);
        }

        public void Delete(int id)
        {
            TEntity entity = Find(id);
            _dbSet.Remove(entity);
        }
    }
}