using System;
using System.Runtime.Serialization;

namespace BankPro.Util.Data.Exceptions
{
    [Serializable]
    public class DbDetailExistsException : Exception
    {
        public DbDetailExistsException()
        {
        }

        public DbDetailExistsException(string message)
            : base(message)
        {
        }

        public DbDetailExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DbDetailExistsException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
        {
        }

        protected DbDetailExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
