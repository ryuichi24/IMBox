using System;

namespace IMBox.Services.Comment.API.DTOs
{
    public record CommentDTO
    {
        public Guid Id { get; init; }
        public Guid MovieId { get; init; }
        public Guid CommenterId { get; init; }
        public string Text { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public CommenterDTO Commenter { get; set; }
    }
}