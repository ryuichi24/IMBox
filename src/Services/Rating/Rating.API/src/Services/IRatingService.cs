using System;
using System.Threading.Tasks;

namespace IMBox.Services.Rating.API.Services
{
    interface IRatingService
    {
        Task<object> GetByMovieIdWithDemographicType(Guid movieId, string demographicType);
    }
}