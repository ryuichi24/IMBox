using System;

namespace IMBox.Services.Movie.API.DTOs
{
    public record MemberDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Role { get; init; }
    }
}