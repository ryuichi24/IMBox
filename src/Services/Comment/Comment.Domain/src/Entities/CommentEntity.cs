using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Comment.Domain.Entities
{
    public class CommentEntity : Entity
    {
        public string Text { get; set; }
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }

        public CommentEntity UpdateText(string newText)
        {
            if (String.IsNullOrEmpty(newText)) return this;
            Text = newText;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}