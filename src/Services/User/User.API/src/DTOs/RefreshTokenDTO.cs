using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.User.API.DTOs
{
    public record RefreshTokenDTO
    {
        [Required]
        public string RefreshToken { get; init; }
    }
}