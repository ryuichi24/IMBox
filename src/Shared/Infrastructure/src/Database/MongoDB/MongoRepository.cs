using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IMBox.Shared.Core.Entity;
using MongoDB.Driver;

namespace IMBox.Shared.Infrastructure.Database.MongoDB
{
    abstract class MongoRepository<TEntity> where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _dbCollection;
        private readonly FilterDefinitionBuilder<TEntity> _filterBuilder = Builders<TEntity>.Filter;

        protected MongoRepository(IMongoDatabase database, string collectionName)
        {
            _dbCollection = database.GetCollection<TEntity>(collectionName);
        }

        protected async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        protected async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }

        protected async Task<TEntity> GetAsync(Guid id)
        {
            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entityFromDB => entityFromDB.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        protected async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbCollection.Find(filter).SingleOrDefaultAsync();
        }

        protected async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbCollection.InsertOneAsync(entity);
        }

        protected async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entityFromDB => entityFromDB.Id, entity.Id);
            await _dbCollection.ReplaceOneAsync(filter, entity);
        }

        protected async Task RemoveAsync(Guid id)
        {
            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entityFromDB => entityFromDB.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}