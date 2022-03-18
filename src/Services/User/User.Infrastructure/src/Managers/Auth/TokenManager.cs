using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using IMBox.Services.User.Domain.Entities;
using IMBox.Shared.Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace IMBox.Services.User.Infrastructure.Managers.Auth
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

        public AccessToken createAccessToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            };

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var expiresIn = DateTime.UtcNow.AddMinutes(_jwtAuthSettings.AccessTokenExpiryInMin);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresIn,
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtAuthSettings.AccessTokenSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessToken
            {
                Token = token,
                ExpiresIn = expiresIn
            };
        }
    }
}