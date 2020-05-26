using System;

namespace Bank.Application.Exceptions
{
    public class MatchingRolesException : Exception
    {
        public MatchingRolesException()
        {
        }

        public MatchingRolesException(string message)
            : base(message)
        {
        }

        public MatchingRolesException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
