using System;

namespace WebAPI.Exceptions
{
    public class CrudException : Exception
    {
        public CrudException() : base() { }
        public CrudException(string message) : base(message) { }
        public CrudException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}