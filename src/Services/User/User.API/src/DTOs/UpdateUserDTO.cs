using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.User.API.DTOs
{
    public record UpdateUserDTO
    {
        public string Username { get; init; }
        [EmailAddress]
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTime BirthDate { get; init; }
        [StringLength(1)]
        public Char Gender { get; init; }
        public string Continent { get; init; }
    }
}