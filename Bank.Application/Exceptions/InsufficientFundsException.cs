﻿using System;

namespace Bank.Application.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException()
        {
        }

        public InsufficientFundsException(string message)
            : base(message)
        {
        }

        public InsufficientFundsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
