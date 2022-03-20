using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Movie.API.DTOs
{
    public record UpdateMovieDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        [Url]
        public string MainPosterUrl { get; init; }
        [Url]
        public string MainTrailerUrl { get; init; }
        [Url]
        public List<string> OtherPostUrls { get; init; }
        [Url]
        public List<string> OtherTrailerUrls { get; init; }
        public List<Guid> MemberIds { get; init; }
    }

}