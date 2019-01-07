using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectFinal101.Core.Repositories
{
    public interface IBaserepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);


        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);


        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();


        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

    }
}
