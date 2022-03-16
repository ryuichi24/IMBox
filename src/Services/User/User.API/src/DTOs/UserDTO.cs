using System;

namespace IMBox.Services.User.API.DTOs
{
    public record UserDTO
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public DateTime BirthDate { get; init; }
        public Char Gender { get; init; }
        public string Continent { get; init; }
    }
}