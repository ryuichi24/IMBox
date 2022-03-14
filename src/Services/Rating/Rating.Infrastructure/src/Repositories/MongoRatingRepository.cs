using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Rating.Infrastructure.Repositories
{
    public class MongoRatingRepository : MongoRepository<RatingEntity>, IRatingRepository
    {
        public MongoRatingRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public Task CreateAsync(RatingEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<RatingEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RatingEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RatingEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}