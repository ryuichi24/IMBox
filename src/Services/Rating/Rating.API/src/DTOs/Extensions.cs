using IMBox.Services.Rating.Domain.Entities;

namespace IMBox.Services.Rating.API.DTOs
{
    public static class Extensions
    {
        public static MovieDTO ToDTO(this MovieEntity movieEntity)
        {
            return new MovieDTO
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                Description = movieEntity.Description
            };
        }

        public static RatingDTO ToDTO(this RatingEntity ratingEntity, MovieEntity movieEntity)
        {
            return new RatingDTO
            {
                Movie = movieEntity.ToDTO(),
                Rating = ratingEntity.Rating,
            };
        }

        public static RaterDTO ToDTO(this RaterEntity raterEntity)
        {
            return new RaterDTO
            {
                Id = raterEntity.Id,
                Name = raterEntity.Name
            };
        }
    }
}