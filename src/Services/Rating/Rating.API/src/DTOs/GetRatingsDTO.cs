using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Rating.API.DTOs
{
    public record GetRatingsDTO
    {
        [Required]
        [FromQuery(Name = "movie_id")]
        public Guid MovieId { get; init; }

        [FromQuery(Name = "demographic")]
        public string DemographicType { get; init; } = null;
    }
}