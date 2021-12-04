using System;

namespace YLunch.Api.Core
{
    public class TokenReadDto
    {
        public string Token { get; init; }
        public DateTime Expiration { get; init; }
    }
}
