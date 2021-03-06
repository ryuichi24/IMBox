using System;
using System.Collections.Generic;

namespace IMBox.Services.Member.API.DTOs
{
    public record MemberDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string HeadshotUrl { get; init; }
        public DateTime BirthDate { get; init; }
        public string Role { get; init; }
        public IEnumerable<MovieDTO> Movies { get; init; }
    }

}