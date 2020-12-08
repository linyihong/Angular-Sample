using System;

namespace Util.Data
{
    public interface IDbContextLogger
    {
        Action<string> Log { get; }
    }
}
