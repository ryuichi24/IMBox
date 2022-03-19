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
                CommenterId = commentEntity.CommenterId,
                Text = commentEntity.Text,
                CreatedAt = commentEntity.CreatedAt
            };
        }

        public static CommentDTO ToDTO(this CommentEntity commentEntity, CommenterEntity commenterEntity)
        {
            return new CommentDTO
            {
                Id = commentEntity.Id,
                MovieId = commentEntity.MovieId,
                Text = commentEntity.Text,
                CreatedAt = commentEntity.CreatedAt,
                CommenterId = commentEntity.CommenterId,
                Commenter = new CommenterDTO
                {
                    Id = commenterEntity.Id,
                    Name = commenterEntity.Name,
                    Gender = commenterEntity.Gender,
                    BirthDate = commenterEntity.BirthDate,
                    Country = commenterEntity.Country
                }
            };
        }
    }
}