using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Rating.Infrastructure.Repositories
{
    public class MongoMovieRepository : MongoRepository<MovieEntity>, IMovieRepository
    {
        public MongoMovieRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public Task CreateAsync(MovieEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MovieEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MovieEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}