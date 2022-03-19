using System;

namespace IMBox.Services.Rating.API.DTOs
{
    public record RaterDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}