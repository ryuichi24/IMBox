using System;
using IMBox.Services.User.Domain.Entities;

namespace IMBox.Services.User.Infrastructure.Managers.Auth
{
    public interface ITokenManager
    {
        AccessToken createAccessToken(UserEntity user);
        RefreshToken createRefreshToken(Guid userId);
        Guid VerifyRefreshToken(string token);
        void RevokeRefreshToken(string token);
        string CreateEmailConfirmToken(Guid userId);
        Guid VerifyEmailConfirmToken(string token);
        void RevokeEmailConfirmToken(string token);
    }
}