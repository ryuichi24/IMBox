using System;

namespace IMBox.Services.User.API.DTOs
{
    public record UpdateUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Char Gender { get; set; }
        public string Continent { get; set; }
    }
}