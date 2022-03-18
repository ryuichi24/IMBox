using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Comment.Infrastructure.Repositories
{
    public class MongoCommenterRepository : MongoRepository<CommenterEntity>, ICommenterRepository
    {
        public MongoCommenterRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public async Task CreateAsync(CommenterEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<List<CommenterEntity>> GetAllAsync()
        {
            return await base.FindAllAsync();
        }

        public async Task<CommenterEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(CommenterEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}