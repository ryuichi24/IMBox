using System;

namespace IMBox.Services.Comment.API.DTOs
{
    public record CommentDTO
    {
        public Guid Id { get; init; }
        public Guid MovieId { get; init; }
        public Guid UserId { get; set; }
        public string Text { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}