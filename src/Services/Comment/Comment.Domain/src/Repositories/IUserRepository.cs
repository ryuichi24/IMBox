using IMBox.Services.Comment.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Comment.Domain.Repositories
{
    public interface IUserRepository : ICanCreate<UserEntity>, ICanUpdate<UserEntity>, ICanRemove, ICanGetById<UserEntity>, ICanGetAll<UserEntity>
    {
    }
}