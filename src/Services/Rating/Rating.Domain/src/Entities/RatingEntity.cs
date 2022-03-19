using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Rating.Domain.Entities
{
    public class RatingEntity : Entity
    {
        public int Rating { get; set; }
        public Guid MovieId { get; set; }
        public Guid RaterId { get; set; }

        public RatingEntity UpdateRating(int newRating)
        {
            // TODO: validate rating range
            Rating = newRating;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}