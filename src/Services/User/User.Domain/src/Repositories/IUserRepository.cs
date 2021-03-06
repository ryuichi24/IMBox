using System.Threading.Tasks;
using IMBox.Services.User.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.User.Domain.Repositories
{
    public interface IUserRepository : ICanCreate<UserEntity>, ICanUpdate<UserEntity>, ICanRemove, ICanGetById<UserEntity>, ICanGetAll<UserEntity>
    {
        Task<UserEntity> GetByEmailAsync(string email);
        Task<UserEntity> GetByEmailConfirmTokenAsync(string token);
    }
}