using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Shared.Domain.Base;

namespace IMBox.Shared.Domain.Repository
{
    public interface ICanGetAll<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
    }
}