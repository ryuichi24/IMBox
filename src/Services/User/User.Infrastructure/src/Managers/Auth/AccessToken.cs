using System;

namespace IMBox.Services.User.Infrastructure.Managers.Auth
{
    public record AccessToken
    {
        public string Token { get; init; }
        public DateTime ExpiresIn { get; init; }
    }
}