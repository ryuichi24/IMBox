using System;

namespace IMBox.Services.Comment.API.DTOs
{
    public record UpdateCommentDTO
    {
        public string Text { get; init; }
    }
}