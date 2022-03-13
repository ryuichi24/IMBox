using System;
using System.Collections.Generic;

namespace IMBox.Services.Movie.API.DTOs
{
    public record MovieDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<MemberDTO> Members { get; set; }
    }


}