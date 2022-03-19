using System;

namespace IMBox.Services.Rating.API.DTOs
{
    public record RaterDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public DateTime BirthDate { get; init; }
        public string Gender { get; init; }
        public string Country { get; init; }
    }
}