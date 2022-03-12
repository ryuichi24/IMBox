using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Member.API.DTOs
{
    public record UpdateMemberDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public DateTime BirthDate { get; init; }
        [Required]
        public string Role { get; init; }
    }

}