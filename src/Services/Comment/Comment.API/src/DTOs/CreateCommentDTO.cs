using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Comment.API.DTOs
{
    public record CreateCommentDTO
    {
        [Required]
        public Guid MovieId { get; init; }
        [Required]
        public Guid UserId { get; init; }
        [Required]
        public string Text { get; init; }
    }
}