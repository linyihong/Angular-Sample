using BankPro.Util.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Util.Data
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        private IDbContextProxy proxy { get; set; }

        public TDbContext Context { get; private set; }

        public UnitOfWork(IDbContextProxy proxy, TDbContext context)
        {
            Context = context;
            this.proxy = proxy;
            proxy.AttachDbContext(context);
        }

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            return new Repository<TEntity>(Context);
        }

        public IRepository Repository(Type entityType)
        {
            return new Repository(Context, entityType);
        }

        public int Commit()
        {
            try
            {
                var entities = Context.ChangeTracker.Entries()
                .Where(e => new[] { EntityState.Added, EntityState.Modified }.Contains(e.State));
                foreach (var entity in entities)
                {
                    var validationContext = new ValidationContext(entity);
                    Validator.ValidateObject(entity, validationContext);
                }

                return proxy.SaveChanges();
            }
            catch (ValidationException ex)
            {
                throw new DbDetailExistsException(ex.Message, ex);
            }
        }


        public int CommitWithRollback()
        {
            int affectedRowQuantity = 0;

            try
            {
                proxy.SaveChanges();
            }
            catch (Exception)
            {
                proxy.Rollback();
                throw;
            }

            return affectedRowQuantity;
        }

        public void Rollback()
        {
            proxy.Rollback();
        }
    }
}
