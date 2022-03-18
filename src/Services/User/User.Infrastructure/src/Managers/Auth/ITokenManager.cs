using System;
using System.Collections.Generic;
using IMBox.Services.User.Domain.Entities;

namespace IMBox.Services.User.Infrastructure.Managers.Auth
{
    public interface ITokenManager
    {
        AccessToken createAccessToken(UserEntity user);
    }
}