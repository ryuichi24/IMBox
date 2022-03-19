using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Rating.API.DTOs
{
    public record GetRatingsResponseDTO
    {
        public MovieDTO Movie { get; init; }
        public IEnumerable<RatingItem> Ratings { get; init; }
        public double TotalRating { get; init; }
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