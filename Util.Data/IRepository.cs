using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Util.Data
{
    public interface IRepository
    {
        DbSet<object> DbSet { get; }
        object Find(params object[] keyValues);
        object Add(object entity);
        IEnumerable<object> Add(IEnumerable<object> entities);
        object Update(object entity);
        object Update(object entity, string[] excludedColumns);
        IEnumerable<object> Update(IEnumerable<object> entities);
        IEnumerable<object> Update(IEnumerable<object> entities, string[] excludedColumns);
        object Delete(object entity);
        IEnumerable<object> Delete(IEnumerable<object> entities);
        IQueryable GetAll();
        int SaveChanges();
    }
}
