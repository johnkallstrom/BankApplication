using System;

namespace Bank.Web.Exceptions
{
    public class MatchingAccountsException : Exception
    {
        public MatchingAccountsException()
        {
        }

        public MatchingAccountsException(string message)
            : base(message)
        {
        }

        public MatchingAccountsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
