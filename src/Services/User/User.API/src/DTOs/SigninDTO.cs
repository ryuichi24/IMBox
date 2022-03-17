using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.User.API.DTOs
{
    public record SigninDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
    }
}