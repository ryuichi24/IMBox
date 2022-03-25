using System;
using System.Collections.Generic;

namespace IMBox.Services.Movie.API.DTOs
{
    public record MovieDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int CommentCount { get; init; }
        public string MainPosterUrl { get; init; }
        public string MainTrailerUrl { get; init; }
        public List<string> OtherPostUrls { get; init; }
        public List<string> OtherTrailerUrls { get; init; }
        public IEnumerable<MemberDTO> Members { get; init; }
        public IEnumerable<MemberDTO> Directors { get; init; }
        public IEnumerable<MemberDTO> Writers { get; init; }
        public IEnumerable<MemberDTO> Casts { get; init; }
    }


}