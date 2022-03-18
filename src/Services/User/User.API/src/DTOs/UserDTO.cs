using System;
using System.Collections.Generic;

namespace IMBox.Services.User.API.DTOs
{
    public record UserDTO
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public DateTime BirthDate { get; init; }
        public string Gender { get; init; }
        public string Continent { get; init; }
        public IEnumerable<string> Roles { get; init; }
    }
}