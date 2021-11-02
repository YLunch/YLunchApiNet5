using System;
using System.Runtime.Serialization;

namespace YLunch.Infrastructure.Database.Exceptions
{
    [Serializable]
    public sealed class UserCreationException : Exception
    {
        private UserCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserCreationException() : base("User creation exception")
        {
        }
    }
}
