using System.Runtime.Serialization;

namespace WBlog.Application.Core.Exceptions
{
    [Serializable]
    public class ObjectNotFoundExeption : Exception
    {
        public ObjectNotFoundExeption()
        {
        }

        public ObjectNotFoundExeption(string? message) : base(message)
        {
        }

        public ObjectNotFoundExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ObjectNotFoundExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}