using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using Util.Data.EFExtensions;

namespace Util.Data
{
    public class DbContextProxy : IDbContextProxy
    {
        private readonly IList<DbContext> dbContextCollection;

        public DbContextProxy()
        {
            dbContextCollection = new List<DbContext>();
        }

        public void AttachDbContext(DbContext context)
        {
            if (context != null && !IsExistInCollection(context))
            {
                dbContextCollection.Add(context);
            }
        }

        private bool IsExistInCollection(DbContext db)
        {
            return dbContextCollection.Count > 0 && dbContextCollection.IndexOf(db) >= 0;
        }

        public void Rollback()
        {
            foreach (var db in dbContextCollection)
            {
                RollbackDb(db);
            }
        }

        private static void RollbackDb(DbContext db)
        {
            var dbEntities = db.ChangeTracker.Entries().AsEnumerable();

            foreach (var dbEntity in dbEntities)
            {
                RollbackEntity(db, dbEntity);
            }
        }

        private static void RollbackEntity(DbContext db, EntityEntry dbEntity)
        {
            var type = dbEntity.Entity.GetType();
            dbEntity.State = EntityState.Detached;

            db.Set(type).Remove(dbEntity);
        }





        public int SaveChanges()
        {
            int affectedRowQuantity = 0;

            foreach (var item in dbContextCollection)
            {
                affectedRowQuantity += item.SaveChanges();
            }

            return affectedRowQuantity;
        }
    }
}
