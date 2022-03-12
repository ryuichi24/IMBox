using System.Threading.Tasks;
using IMBox.Shared.Core.Entity;

namespace IMBox.Shared.Database.Helpers
{
    public interface ICanCreate<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entity);
    }
}