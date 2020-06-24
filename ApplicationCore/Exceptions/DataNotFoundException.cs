using System;

namespace ApplicationCore.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(int Id) : base($"Data not found with id {Id}")
        {
        }

        protected DataNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public DataNotFoundException(string message) : base(message)
        {
        }

        public DataNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
