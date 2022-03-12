using System.Threading.Tasks;
using IMBox.Shared.Domain.Base;

namespace IMBox.Shared.Domain.Repository
{
    public interface ICanUpdate<TEntity> where TEntity : Entity
    {
        Task UpdateAsync(TEntity entity);
    }
}