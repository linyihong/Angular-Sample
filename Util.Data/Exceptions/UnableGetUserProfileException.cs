using System;
using System.Runtime.Serialization;

namespace BankPro.Util.Data.Exceptions
{
    [Serializable]
    public class UnableGetUserProfileException : Exception
    {
        public UnableGetUserProfileException()
        {
        }

        public UnableGetUserProfileException(string message)
            : base(message)
        {
        }

        public UnableGetUserProfileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnableGetUserProfileException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
        {
        }

        protected UnableGetUserProfileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
