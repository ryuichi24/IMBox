using System;
using System.Threading.Tasks;

namespace IMBox.Shared.Infrastructure.Database.Helpers
{
    public interface ICanGetById
    {
        Task GetByIdAsync(Guid id);
    }
}