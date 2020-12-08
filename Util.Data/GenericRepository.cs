using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Util.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext context;
        private readonly string[] createInfoColumns;
        private readonly IEnumerable<string> propertyNames;

        public DbSet<TEntity> DbSet { get; private set; }

        public Repository(DbContext context)
        {
            this.context = context;
            DbSet = this.context.Set<TEntity>();
            createInfoColumns = new string[] { "CreateId", "CreateTime" };
            propertyNames = typeof(TEntity).GetProperties().Select(x => x.Name);
        }

        public TEntity Find(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            RevertCreateInfoColumns(entity);
            return entity;
        }

        public TEntity Update(TEntity entity, string[] excludedColumns)
        {
            context.Entry(entity).State = EntityState.Modified;
            RevertCreateInfoColumns(entity);
            RevertExcludedColumns(entity, excludedColumns);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            DbSet.Add(entity);
            context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);

            return DbSet;
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }

            return entities;
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, string[] excludedColumns)
        {
            foreach (var entity in entities)
            {
                Update(entity, excludedColumns);
            }

            return entities;
        }

        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities.ToArray())
                {
                    Delete(entity);
                }
            }

            return entities;
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        private void RevertCreateInfoColumns(TEntity entity)
        {
            var revertColumns = propertyNames.Intersect(createInfoColumns);

            foreach (var revertColumn in revertColumns)
            {
                context.Entry(entity).Property(revertColumn).IsModified = false;
            }
        }

        private void RevertExcludedColumns(TEntity entity, string[] excludedColumns)
        {
            var revertColumns = propertyNames.Intersect(excludedColumns);

            foreach (var revertColumn in revertColumns)
            {
                context.Entry(entity).Property(revertColumn).IsModified = false;
            }
        }
    }
}
