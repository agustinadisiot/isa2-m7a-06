using System;

namespace MinTur.Exceptions
{
    public class InvalidRequestDataException : Exception
    {
        protected InvalidRequestDataException() { }

        public InvalidRequestDataException(string message) : base(message) { }
    }
}
