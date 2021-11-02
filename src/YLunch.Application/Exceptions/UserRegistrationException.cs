using System;
using System.Runtime.Serialization;

namespace YLunch.Application.Exceptions
{
    [Serializable]
    public sealed class UserRegistrationException : Exception
    {
        private UserRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserRegistrationException() : base("User creation exception")
        {
        }
    }
}
