using System;

namespace Bank.Application.Exceptions
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
