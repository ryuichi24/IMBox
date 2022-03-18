using System.ComponentModel.DataAnnotations;
using IMBox.Services.User.Infrastructure.Managers.Auth;

namespace IMBox.Services.User.API.DTOs
{
    public record TokenResponseDTO
    {
        public AccessToken AccessToken { get; init; }
        public RefreshToken RefreshToken { get; init; }
    }
}