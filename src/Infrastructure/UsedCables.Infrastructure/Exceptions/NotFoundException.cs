﻿namespace UsedCables.Infrastructure.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }
}