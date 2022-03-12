using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IMBox.Shared.Domain.Base;
using MongoDB.Driver;

namespace IMBox.Shared.Infrastructure.Database.MongoDB
{
    public abstract class MongoRepository<TEntity> where TEntity : Entity
    {
        private readonly IMongoCollection<TEntity> _dbCollection;
        private readonly FilterDefinitionBuilder<TEntity> _filterBuilder = Builders<TEntity>.Filter;

        protected MongoRepository(IMongoDatabase database, string collectionName)
        {
            _dbCollection = database.GetCollection<TEntity>(collectionName);
        }

        protected async Task<List<TEntity>> FindAllAsync()
        {
            try
            {
                return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
            }
            catch (System.Exception err)
            {
                throw err;
            }

        }

        protected async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }

        protected async Task<TEntity> FindByIdAsync(Guid id)
        {
            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entityFromDB => entityFromDB.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        protected async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbCollection.Find(filter).SingleOrDefaultAsync();
        }

        protected async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbCollection.InsertOneAsync(entity);
        }

        protected async Task ReplaceAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entityFromDB => entityFromDB.Id, entity.Id);
            await _dbCollection.ReplaceOneAsync(filter, entity);
        }

        protected async Task DeleteAsync(Guid id)
        {
            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entityFromDB => entityFromDB.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}