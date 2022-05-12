using System;

namespace MinTur.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        protected ResourceNotFoundException() { }

        public ResourceNotFoundException(string message) : base(message) { }
    }
}
