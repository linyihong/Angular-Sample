using Microsoft.EntityFrameworkCore;

namespace Util.Data
{
    public interface IDbContextProxy
    {
        void AttachDbContext(DbContext context);
        void Rollback();
        int SaveChanges();
    }
}
