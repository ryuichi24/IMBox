using System;
using System.Collections.Generic;
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
        public string Gender { get; init; }
        public string Country { get; init; }
        public IEnumerable<string> Roles { get; init; }
    }
}