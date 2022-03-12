using System;
using System.Threading.Tasks;

namespace IMBox.Shared.Infrastructure.Database.Helpers
{
    public interface ICanRemove
    {
        Task RemoveAsync(Guid id);
    }
}