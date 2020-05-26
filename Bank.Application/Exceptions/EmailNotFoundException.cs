using System;

namespace Bank.Application.Exceptions
{
    public class EmailNotFoundException : Exception
    {
        public EmailNotFoundException()
        {
        }

        public EmailNotFoundException(string message)
            : base(message)
        {
        }

        public EmailNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
