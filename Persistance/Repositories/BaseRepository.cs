using Microsoft.EntityFrameworkCore;
using ProjectFinal101.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectFinal101.Persistance.Repositories
{
    public class BaseRepository<TEntity> : IBaserepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(DbContext context)
        {
            _entities = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var addRange = entities as TEntity[] ?? entities.ToArray();
            _entities.AddRange(addRange);

            return addRange;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }
    }
}
