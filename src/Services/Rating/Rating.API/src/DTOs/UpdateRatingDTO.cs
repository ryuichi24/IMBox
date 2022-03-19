using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Rating.API.DTOs
{
    public record UpdateRatingDTO
    {
        [Range(1, 5)]
        public int Rating { get; init; }
    }
}