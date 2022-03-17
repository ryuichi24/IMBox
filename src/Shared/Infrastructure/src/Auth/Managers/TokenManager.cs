using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace IMBox.Shared.Infrastructure.Auth.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly JwtAuthSettings _jwtAuthSettings;
        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtAuthSettings = _configuration.GetSection(nameof(JwtAuthSettings)).Get<JwtAuthSettings>();
        }

        public string createAccessToken(Guid userId, IEnumerable<string> roles = default(IEnumerable<string>))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtAuthSettings.AccessTokenExpiryInMin),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtAuthSettings.AccessTokenSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}