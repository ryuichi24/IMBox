using System;

namespace IMBox.Services.User.API.DTOs
{
    public record UpdateUserDTO
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTime BirthDate { get; init; }
        public Char Gender { get; init; }
        public string Continent { get; init; }
    }
}