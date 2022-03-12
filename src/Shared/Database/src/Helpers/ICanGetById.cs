using System;
using System.Threading.Tasks;

namespace IMBox.Shared.Database.Helpers
{
    public interface ICanGetById
    {
        Task GetByIdAsync(Guid id);
    }
}