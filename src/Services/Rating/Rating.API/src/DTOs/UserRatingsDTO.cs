using System.Collections.Generic;

namespace IMBox.Services.Rating.API.DTOs
{
    public record UserRatingsDTO
    {
        public RaterDTO Rater { get; init; }
        public IEnumerable<RatingDTO> Ratings { get; init; }
    }
}