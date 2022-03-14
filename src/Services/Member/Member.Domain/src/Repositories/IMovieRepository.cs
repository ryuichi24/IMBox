
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Member.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Member.Domain.Repositories
{
    public interface IMovieRepository : ICanCreate<MovieEntity>, ICanUpdate<MovieEntity>, ICanRemove, ICanGetById<MovieEntity>, ICanGetAll<MovieEntity>
    {
        Task<IEnumerable<MovieEntity>> GetAllByMemberIdAsync(Guid memberId);
    }
}