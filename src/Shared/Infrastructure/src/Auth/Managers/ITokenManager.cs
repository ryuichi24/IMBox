using System;
using System.Collections.Generic;

namespace IMBox.Shared.Infrastructure.Auth.Managers
{
    public interface ITokenManager
    {
        string createAccessToken(Guid userId, IEnumerable<string> roles = default(IEnumerable<string>));
    }
}