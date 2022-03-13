using IMBox.Services.Movie.Domain.Entities;

namespace IMBox.Services.Movie.API.DTOs
{
    public static class Extensions
    {
        public static MovieDTO ToDTO(this MovieEntity MovieEntity)
        {
            return new MovieDTO
            {
                Id = MovieEntity.Id,
                Title = MovieEntity.Title,
                Description = MovieEntity.Description,
            };
        }
    }
}