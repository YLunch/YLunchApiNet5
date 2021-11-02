using System;
using System.Runtime.Serialization;

namespace YLunch.Application.Exceptions
{
    [Serializable]
    public sealed class BadNewOrderStateException : Exception
    {
        private BadNewOrderStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BadNewOrderStateException(string message) : base(message)
        {
        }
    }
}
