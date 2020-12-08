using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Util.Data.EFExtensions
{
    public static class DbContextExt
    {
        public static IEnumerable<T> GetAll<T>(this DbContext dbContext) where T : class
        {
            var tableName = GetTableName<T>();
            var columnMappings = GetColumnMappings<T>();
            var sqlCommand = $"SELECT {columnMappings} FROM {tableName}";

            return dbContext.Set<T>().FromSqlRaw(sqlCommand);
        }

        private static object GetColumnMappings<T>() where T : class
        {
            var columns = from p in typeof(T).GetProperties()
                          select $"{GetColumnAttribute(p).Name} AS {p.Name}";

            return string.Join(",", columns);
        }

        private static string GetTableName<T>() where T : class
        {
            TableAttribute attr = GetTableAttribute<T>();
            return attr.Name;
        }

        private static ColumnAttribute GetColumnAttribute(System.Reflection.PropertyInfo p)
        {
            return p.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
        }

        private static TableAttribute GetTableAttribute<T>() where T : class
        {
            return typeof(T).GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault() as TableAttribute;
        }
    }
}
