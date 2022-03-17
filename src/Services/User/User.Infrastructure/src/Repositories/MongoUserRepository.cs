using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.User.Domain.Entities;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.User.Infrastructure.Repositories
{
    public class MongoUserRepository : MongoRepository<UserEntity>, IUserRepository
    {
        public MongoUserRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public async Task CreateAsync(UserEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await base.FindAllAsync();
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            return await base.FindAsync(user => user.Email == email);
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(UserEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}