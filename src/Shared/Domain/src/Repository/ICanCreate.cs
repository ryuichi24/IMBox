using System.Threading.Tasks;
using IMBox.Shared.Domain.Base;

namespace IMBox.Shared.Domain.Repository
{
    public interface ICanCreate<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);
    }
}