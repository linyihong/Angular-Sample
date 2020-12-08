using Microsoft.EntityFrameworkCore;
using System;

namespace Util.Data.EFExtensions
{
    public static class DbSetExt
    {
        public static DbSet<object> Set(this DbContext _context, Type t)
        {
            return (DbSet<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}
