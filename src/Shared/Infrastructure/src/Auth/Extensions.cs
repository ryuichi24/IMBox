using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IMBox.Shared.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IMBox.Shared.Infrastructure.Helpers.Auth
{
    public static class Extensions
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var jwtAuthSettings = configuration.GetSection(nameof(JwtAuthSettings)).Get<JwtAuthSettings>();
            var jwtSecret = jwtAuthSettings.AccessTokenSecret;

            services.AddAuthentication(authenticationConfig =>
            {
                authenticationConfig.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authenticationConfig.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtConfig =>
            {
                jwtConfig.RequireHttpsMetadata = false;
                jwtConfig.SaveToken = true;
                jwtConfig.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static string SubjectId(this ClaimsPrincipal user)
        {
            // dotnet automatically converts JwtRegisteredClaimNames.Sub into ClaimTypes.NameIdentifier
            return user?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
        }
    }
}