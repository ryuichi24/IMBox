using IMBox.Services.Rating.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Rating.Domain.Repositories
{
    public interface IMovieRepository : ICanCreate<MovieEntity>, ICanUpdate<MovieEntity>, ICanRemove, ICanGetById<MovieEntity>, ICanGetAll<MovieEntity>
    {
    }
}