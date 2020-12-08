using Microsoft.EntityFrameworkCore;
using System;

namespace Util.Data
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        TDbContext Context { get; }
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IRepository Repository(Type entityType);
        int Commit();
        int CommitWithRollback();
        void Rollback();
    }
}
