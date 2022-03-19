using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Rating.Infrastructure.Repositories
{
    public class MongoRaterRepository : MongoRepository<RaterEntity>, IRaterRepository
    {
        public MongoRaterRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public async Task CreateAsync(RaterEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<List<RaterEntity>> GetAllAsync()
        {
            return await base.FindAllAsync();
        }

        public async Task<RaterEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(RaterEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}