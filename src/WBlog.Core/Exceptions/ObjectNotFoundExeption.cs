using System.Runtime.Serialization;

namespace WBlog.Core.Exceptions
{
    [Serializable]
    internal class ObjectNotFoundExeption : InvalidOperationException
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