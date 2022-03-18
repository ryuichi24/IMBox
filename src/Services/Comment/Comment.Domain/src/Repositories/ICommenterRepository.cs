using IMBox.Services.Comment.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Comment.Domain.Repositories
{
    public interface ICommenterRepository : ICanCreate<CommenterEntity>, ICanUpdate<CommenterEntity>, ICanRemove, ICanGetById<CommenterEntity>, ICanGetAll<CommenterEntity>
    {
    }
}