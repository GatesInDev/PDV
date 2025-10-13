using System;

namespace PDV.Core.Exceptions
{
    public class UserRepositoryException : Exception
    {
        public UserRepositoryException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}