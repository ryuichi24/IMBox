using IMBox.Services.Comment.Domain.Entities;

namespace IMBox.Services.Comment.API.DTOs
{
    public static class Extensions
    {
        public static CommentDTO ToDTO(this CommentEntity commentEntity)
        {
            return new CommentDTO
            {
                Id = commentEntity.Id,
                MovieId = commentEntity.MovieId,
                UserId = commentEntity.UserId,
                Text = commentEntity.Text,
                CreatedAt = commentEntity.CreatedAt
            };
        }
    }
}