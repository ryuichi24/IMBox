using System;

namespace IMBox.Services.Rating.API.DTOs
{
    public record RatingDTO
    {
        public Guid Id { get; init; }
        public int Rating { get; init; }
        public MovieDTO Movie { get; init; }
    }
}