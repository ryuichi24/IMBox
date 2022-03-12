using System;
using System.Threading.Tasks;
using IMBox.Shared.Domain.Base;

namespace IMBox.Shared.Domain.Repository
{
    public interface ICanGetById<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(Guid id);
    }
}