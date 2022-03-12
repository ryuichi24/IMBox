using System;
using System.Threading.Tasks;

namespace IMBox.Shared.Domain.Repository
{
    public interface ICanRemove
    {
        Task RemoveAsync(Guid id);
    }
}