using System;
using System.Runtime.Serialization;

namespace YLunch.Application.Exceptions
{
    [Serializable]
    public sealed class UserAlreadyExistsException : Exception
    {
        private UserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserAlreadyExistsException() : base("User already exists")
        {
        }
    }
}
