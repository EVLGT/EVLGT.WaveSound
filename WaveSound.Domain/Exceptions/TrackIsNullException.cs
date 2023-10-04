using System.Runtime.Serialization;

namespace WaveSound.Domain.Exceptions
{
    [Serializable]
    public class TrackIsNullException : Exception
    {
        public TrackIsNullException()
        {
        }

        public TrackIsNullException(string message) : base(message)
        {
        }

        public TrackIsNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TrackIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}