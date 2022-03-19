using System.Collections.Generic;

namespace IMBox.Services.Rating.API.DTOs
{
    public class GetByMovieIdResponseDTO
    {
        public MovieDTO Movie { get; init; }
        public List<RatingSummaryItem> Ratings { get; init; } = new List<RatingSummaryItem>
        {
            new RatingSummaryItem
            {
                Rating = 1,
                RatingVoteCount = 0,
                Percent = "0%"
            },
            new RatingSummaryItem
            {
                Rating = 2,
                RatingVoteCount = 0,
                Percent = "0%"
            },
            new RatingSummaryItem
            {
                Rating = 3,
                RatingVoteCount = 0,
                Percent = "0%"
            },
            new RatingSummaryItem
            {
                Rating = 4,
                RatingVoteCount = 0,
                Percent = "0%"
            },
            new RatingSummaryItem
            {
                Rating = 5,
                RatingVoteCount = 0,
                Percent = "0%"
            },
        };
        public double AverageRating { get; set; }
        public int TotalRatingVoteCount { get; init; }
        public string DemographicType { get; init; }
    }

    public record RatingSummaryItem
    {
        public int Rating { get; init; }
        public string Percent { get; set; }
        public int RatingVoteCount { get; set; }

        public RatingSummaryItem UpdatePercent(int totalRatingVoteCount)
        {
            if(totalRatingVoteCount == 0) return this;
            Percent = $"{(RatingVoteCount / totalRatingVoteCount) * 100}%";
            return this;
        }

        public RatingSummaryItem IncrementRatingVoteCount()
        {
            RatingVoteCount++;
            return this;
        }

        public RatingSummaryItem DecrementRatingVoteCount()
        {
            RatingVoteCount--;
            return this;
        }
    }
}