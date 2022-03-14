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

        public Task CreateAsync(CommentEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommentEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CommentEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}