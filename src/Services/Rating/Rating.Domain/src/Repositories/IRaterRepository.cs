using IMBox.Services.Rating.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Rating.Domain.Repositories
{
    public interface IRaterRepository : ICanCreate<RaterEntity>, ICanUpdate<RaterEntity>, ICanRemove, ICanGetById<RaterEntity>, ICanGetAll<RaterEntity>
    {
    }
}