using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Util.Data.EFExtensions;

namespace Util.Data
{
    public class Repository : IRepository
    {
        public DbSet<object> DbSet { get; private set; }

        private readonly DbContext context;
        private readonly string[] createInfoColumns;
        private readonly IEnumerable<string> propertyNames;

        public Repository(DbContext context, Type entityType)
        {
            this.context = context;
            DbSet = this.context.Set(entityType);
            createInfoColumns = new string[] { "CreateId", "CreateTime" };
            propertyNames = entityType.GetProperties().Select(x => x.Name);
        }

        public object Add(object entity)
        {
            return DbSet.Add(entity);
        }

        public IEnumerable<object> Add(IEnumerable<object> entities)
        {
            DbSet.AddRange(entities);

            return DbSet.OfType<object>();
        }

        public object Delete(object entity)
        {
            DbSet.Add(entity);
            context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public IEnumerable<object> Delete(IEnumerable<object> entities)
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

        public object Find(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public IQueryable GetAll()
        {
            return DbSet;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public object Update(object entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            RevertCreateInfoColumns(entity);
            return entity;
        }

        public object Update(object entity, string[] excludedColumns)
        {
            context.Entry(entity).State = EntityState.Modified;
            RevertCreateInfoColumns(entity);
            RevertExcludedColumns(entity, excludedColumns);
            return entity;
        }

        public IEnumerable<object> Update(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }

            return entities;
        }

        public IEnumerable<object> Update(IEnumerable<object> entities, string[] excludedColumns)
        {
            foreach (var entity in entities)
            {
                Update(entity, excludedColumns);
            }

            return entities;
        }

        private void RevertCreateInfoColumns(object entity)
        {
            var revertColumns = propertyNames.Intersect(createInfoColumns);

            foreach (var revertColumn in revertColumns)
            {
                context.Entry(entity).Property(revertColumn).IsModified = false;
            }
        }

        private void RevertExcludedColumns(object entity, string[] excludedColumns)
        {
            var revertColumns = propertyNames.Intersect(excludedColumns);

            foreach (var revertColumn in revertColumns)
            {
                context.Entry(entity).Property(revertColumn).IsModified = false;
            }
        }
    }
}
