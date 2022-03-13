using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Movie.API.DTOs
{
    public record UpdateMovieDTO
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Description { get; init; }
    }

}