using System;

namespace IMBox.Services.User.API.DTOs
{
    public record UserDTO
    {
        public Guid Id { get; init; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Char Gender { get; set; }
        public string Continent { get; set; }
    }
}