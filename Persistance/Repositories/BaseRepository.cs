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
        protected readonly DbSet<TEntity> Entities;

        protected readonly ApplicationDbContext Context;

        public BaseRepository(DbContext context)
        {
            Entities = context.Set<TEntity>();
            Context = context as ApplicationDbContext;

        }

        public TEntity Add(TEntity entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var addRange = entities as TEntity[] ?? entities.ToArray();
            Entities.AddRange(addRange);

            return addRange;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }
    }
}
