using System;

namespace IMBox.Services.User.Infrastructure.Managers.Auth
{
    public record RefreshToken
    {
        public string Token { get; init; }
        public DateTime ExpiresIn { get; init; }
    }
}