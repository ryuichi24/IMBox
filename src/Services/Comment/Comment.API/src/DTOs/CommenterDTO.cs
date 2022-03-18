using System;

namespace IMBox.Services.Comment.API.DTOs
{
    public record CommenterDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public DateTime BirthDate { get; init; }
        public string Gender { get; init; }
        public string Continent { get; init; }
    }
}