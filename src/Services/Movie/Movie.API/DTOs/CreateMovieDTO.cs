using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Movie.API.DTOs
{
    public record CreateMovieDTO
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Description { get; init; }
    }

}