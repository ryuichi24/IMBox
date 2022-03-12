using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Member.Domain.Entities;
using IMBox.Services.Member.Domain.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using MongoDB.Driver;

namespace IMBox.Services.Member.Infrastructure.Repositories
{
    public class MongoMemberRepository : MongoRepository<MemberEntity>, IMemberRepository
    {
        public MongoMemberRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public async Task CreateAsync(MemberEntity entity)
        {
            await base.InsertAsync(entity);
        }

        public async Task<List<MemberEntity>> GetAllAsync()
        {
            return await base.FindAllAsync();
        }

        public async Task<MemberEntity> GetByIdAsync(Guid id)
        {
            return await base.FindByIdAsync(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task UpdateAsync(MemberEntity entity)
        {
            await base.ReplaceAsync(entity);
        }
    }
}