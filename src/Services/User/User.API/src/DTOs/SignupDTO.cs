using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.User.API.DTOs
{
    public record SignupDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}