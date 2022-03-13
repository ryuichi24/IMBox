using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Movie.API.DTOs
{
    public record UpdateMovieDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public List<Guid> MemberIds { get; set; }
    }

}