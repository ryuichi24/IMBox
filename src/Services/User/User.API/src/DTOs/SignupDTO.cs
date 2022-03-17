using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.User.API.DTOs
{
    public record SignupDTO
    {
        [Required]
        public string Username { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
    }
}