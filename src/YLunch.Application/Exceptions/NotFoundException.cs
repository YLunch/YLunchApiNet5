using System;
using System.Runtime.Serialization;

namespace YLunch.Application.Exceptions
{
    [Serializable]
    public sealed class NotFoundException : Exception
    {
        private NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException() : base("Entity not found exception")
        {
        }
        public NotFoundException(string message) : base($"Entity not found exception: {message}")
        {
        }
    }
}
