using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Comment.Domain.Repositories
{
    public interface ICommentRepository : ICanCreate<CommentEntity>, ICanUpdate<CommentEntity>, ICanRemove, ICanGetById<CommentEntity>, ICanGetAll<CommentEntity>
    {
        Task<IEnumerable<CommentEntity>> GetAllByMovieIdAsync(Guid movieId);
    }
}