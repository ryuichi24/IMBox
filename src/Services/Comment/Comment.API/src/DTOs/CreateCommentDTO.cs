using System;

namespace IMBox.Services.Comment.API.DTOs
{
    public record CreateCommentDTO
    {
        public Guid MovieId { get; init; }
        public Guid UserId { get; set; }
        public string Text { get; init; }
    }
}