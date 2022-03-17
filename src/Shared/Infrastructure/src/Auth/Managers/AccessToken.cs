using System;

namespace IMBox.Shared.Infrastructure.Auth.Managers
{
    public record AccessToken
    {
        public string Token { get; init; }
        public DateTime ExpiresIn { get; init; }
    }
}