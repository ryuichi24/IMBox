using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Rating.Domain.Repositories
{
    public interface IRatingRepository : ICanCreate<RatingEntity>, ICanUpdate<RatingEntity>, ICanRemove, ICanGetById<RatingEntity>
    {
        Task<IEnumerable<RatingEntity>> GetAllByMovieId(Guid movieId);
    }
}