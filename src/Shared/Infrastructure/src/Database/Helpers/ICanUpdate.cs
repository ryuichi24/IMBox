using System.Threading.Tasks;
using IMBox.Shared.Core.Entity;

namespace IMBox.Shared.Infrastructure.Database.Helpers
{
    public interface ICanUpdate<TEntity> where TEntity : IEntity
    {
        Task UpdateAsync(TEntity entity);
    }
}