using System;

namespace Bank.Web.Exceptions
{
    public class NoNegativeAmountException : Exception
    {
        public NoNegativeAmountException()
        {
        }

        public NoNegativeAmountException(string message)
            : base(message)
        {
        }

        public NoNegativeAmountException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
