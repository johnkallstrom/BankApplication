using System;

namespace Bank.Web.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
        {
        }

        public AccountNotFoundException(string message)
            : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
