using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Member.API.DTOs
{
    public record UpdateMemberDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        [Url]
        public string HeadshotUrl { get; init; }
        public DateTime BirthDate { get; init; }
        public string Role { get; init; }
    }

}