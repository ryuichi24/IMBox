using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Comment.Infrastructure.Repositories
{
    public class MongoCommentRepository : MongoRepository<CommentEntity>, ICommentRepository
    {
        public MongoCommentRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public async Task CreateAsync(CommentEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<List<CommentEntity>> GetAllAsync()
        {
            return await base.FindAllAsync();
        }

        public async Task<IEnumerable<CommentEntity>> GetAllByMovieIdAsync(Guid movieId)
        {
            return await base.FindAllAsync(comment => comment.MovieId == movieId);
        }

        public async Task<CommentEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(CommentEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}