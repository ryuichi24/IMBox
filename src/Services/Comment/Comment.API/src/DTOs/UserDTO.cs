using System;

namespace IMBox.Services.Comment.API.DTOs
{
    public record UserDTO
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public DateTime BirthDate { get; init; }
        public string Gender { get; init; }
        public string Continent { get; init; }
    }
}