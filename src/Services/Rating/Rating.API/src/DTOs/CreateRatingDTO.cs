using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Rating.API.DTOs
{
    public record CreateRatingDTO
    {
        [Range(1, 5)]
        public int Rating { get; init; }
        public Guid MovieId { get; init; }
    }
}