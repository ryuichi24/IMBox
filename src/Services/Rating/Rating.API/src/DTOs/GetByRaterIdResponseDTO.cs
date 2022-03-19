using System.Collections.Generic;

namespace IMBox.Services.Rating.API.DTOs
{
    public record GetByRaterIdResponseDTO
    {
        public RaterDTO Rater { get; init; }
        public IEnumerable<RatingDTO> Ratings { get; init; }
    }
}