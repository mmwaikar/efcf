using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Entities.Common
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> All { get; }
        IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> where);
        TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> FindMany(Func<TEntity, bool> where);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy, int page, int pageSize, out int rows);

        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy, int page,
            int pageSize, out int rows,
            params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> where, Func<TEntity, object> orderBy,
            string sortOrder,
            int page, int pageSize, out int rows,
            params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Find(int id);
        TEntity Find(int id, params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> FindMany( int id , params Expression<Func<TEntity , object>>[] includeProperties );

        void InsertOrUpdate(TEntity entity);
        void Delete(int id);
    }
}
