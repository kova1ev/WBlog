using System.Runtime.Serialization;

namespace WBlog.Application.Core.Exceptions
{
    [Serializable]
    internal class ObjectExistingException : Exception
    {
        public ObjectExistingException()
        {
        }

        public ObjectExistingException(string? message) : base(message)
        {
        }

        public ObjectExistingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ObjectExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}