using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Movie.Domain.Entities;
using IMBox.Services.Movie.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Movie.Infrastructure.Repositories
{
    public class MongoMovieRepository : MongoRepository<MovieEntity>, IMovieRepository
    {
        public MongoMovieRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public async Task CreateAsync(MovieEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<List<MovieEntity>> GetAllAsync()
        {
            return await base.FindAllAsync();
        }

        public async Task<MovieEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(MovieEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}