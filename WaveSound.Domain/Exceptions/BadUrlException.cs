using System.Runtime.Serialization;

namespace WaveSound.Domain.Exceptions
{
    [Serializable]
    public class BadUrlException : Exception
    {
        public BadUrlException()
        {
        }

        public BadUrlException(string message) : base(message)
        {
        }

        public BadUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}