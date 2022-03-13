using System;

namespace IMBox.Services.Movie.API.DTOs
{
    public record MovieDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }

}