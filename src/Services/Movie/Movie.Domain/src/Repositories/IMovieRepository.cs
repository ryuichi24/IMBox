using IMBox.Services.Movie.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Movie.Domain.Repositories
{
    public interface IMovieRepository : ICanCreate<MovieEntity>, ICanUpdate<MovieEntity>, ICanRemove, ICanGetById<MovieEntity>, ICanGetAll<MovieEntity>
    {
    }
}