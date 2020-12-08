using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Util.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        DbSet<TEntity> DbSet { get; }
        TEntity Find(params object[] keyValues);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        TEntity Update(TEntity entity, string[] excludedColumns);
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, string[] excludedColumns);
        TEntity Delete(TEntity entity);
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetAll();
        int SaveChanges();
    }
}
