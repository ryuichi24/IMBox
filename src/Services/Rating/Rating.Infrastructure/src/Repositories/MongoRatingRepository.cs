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

        public async Task CreateAsync(RatingEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<IEnumerable<RatingEntity>> GetAllByMovieId(Guid movieId)
        {
            return await base.FindAllAsync(rating => rating.MovieId == movieId);
        }

        public async Task<IEnumerable<RatingEntity>> GetAllByRaterId(Guid raterId)
        {
            return await base.FindAllAsync(rating => rating.RaterId == raterId);
        }

        public async Task<RatingEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task<RatingEntity> GetByRaterIdAndMovieId(Guid raterId, Guid movieId)
        {
            return await base.FindAsync(rating => rating.MovieId == movieId && rating.RaterId == raterId);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(RatingEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}