using System;
using System.Collections.Generic;

namespace IMBox.Shared.Infrastructure.Auth.Managers
{
    public interface ITokenManager
    {
        AccessToken createAccessToken(Guid userId, IEnumerable<string> roles = default(IEnumerable<string>));
    }
}