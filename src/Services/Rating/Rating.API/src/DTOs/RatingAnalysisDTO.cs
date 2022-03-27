using System.Collections.Generic;

namespace IMBox.Services.Rating.API.DTOs
{
    public record RatingAnalysisDTO
    {
        public MovieDTO Movie { get; init; }
        public IEnumerable<RatingItem> Ratings { get; init; }
        public double AverageRating { get; init; }
        public int TotalRatingVoteCount { get; init; }
        public string DemographicType { get; init; }

    }
    public record RatingItem
    {
        public int Rating { get; init; }
        public string Percent { get; init; }
        public int RatingVoteCount { get; init; }
    }
}