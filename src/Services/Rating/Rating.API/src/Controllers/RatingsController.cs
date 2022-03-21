using System;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Core.StringHelpers;
using IMBox.Services.Rating.API.DTOs;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Shared.Infrastructure.Helpers.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Rating.API.Controllers
{
    [Authorize]
    [Route("api/rating-service/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRaterRepository _raterRepository;
        private readonly IMovieRepository _movieRepository;
        public RatingsController(IRatingRepository ratingRepository, IRaterRepository raterRepository, IMovieRepository movieRepository)
        {
            _ratingRepository = ratingRepository;
            _raterRepository = raterRepository;
            _movieRepository = movieRepository;
        }

        [AllowAnonymous]
        [Route("/api/rating-service/movies/{movieId}/ratings")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByMovieIdResponseDTO))]
        public async Task<IActionResult> GetByMovieIdAsync([FromRoute] Guid movieId, [FromQuery(Name = "demographic")] string demographicType = "all")
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);

            if (movie == null) return BadRequest("No movie found.");

            var ratings = await _ratingRepository.GetAllByMovieId(movieId);
            var raters = await _raterRepository.GetAllAsync();

            var ratingsWithRaters = ratings.Select(rating =>
            {
                rating.Rater = raters.Find(rater => rater.Id == rating.RaterId);
                return rating;
            });

            var filteredRatings = ratingsWithRaters.Where(BuildRatingsFilter(demographicType)).ToList();

            var dto = new GetByMovieIdResponseDTO
            {
                Movie = movie.ToDTO(),
                DemographicType = demographicType,
                TotalRatingVoteCount = filteredRatings.Count()
            };

            filteredRatings.ForEach(rating =>
            {
                dto.Ratings.ForEach(ratingItem =>
                {
                    if (ratingItem.Rating == rating.Rating) ratingItem.IncrementRatingVoteCount();
                });
            });

            dto.Ratings.ForEach(ratingItem => ratingItem.UpdatePercent(dto.TotalRatingVoteCount));

            dto.AverageRating = dto.Ratings.Select(ratingItem => ratingItem.RatingVoteCount * ratingItem.Rating).Sum() / dto.TotalRatingVoteCount;

            return Ok(dto);
        }

        [AllowAnonymous]
        [Route("/api/rating-service/raters/{raterId}/ratings")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByRaterIdResponseDTO))]
        public async Task<IActionResult> GetByRaterIdAsync([FromRoute] Guid raterId)
        {
            var rater = await _raterRepository.GetByIdAsync(raterId);
            if (rater == null) return NotFound("No rater found.");

            var ratings = await _ratingRepository.GetAllByRaterId(raterId);
            var movies = await _movieRepository.GetAllAsync();

            var ratingDTOs = ratings.Select(rating =>
            {
                var movie = movies.Find(movieItem => movieItem.Id == rating.MovieId);
                return new RatingDTO
                {
                    Movie = movie.ToDTO(),
                    Rating = rating.Rating
                };
            });


            return Ok(new GetByRaterIdResponseDTO
            {
                Rater = rater.ToDTO(),
                Ratings = ratingDTOs
            });
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> CreateAsync(CreateRatingDTO createRatingDTO)
        {
            var newRating = new RatingEntity
            {
                Rating = createRatingDTO.Rating,
                MovieId = createRatingDTO.MovieId,
                RaterId = User.SubjectId().ToGuid()
            };

            await _ratingRepository.CreateAsync(newRating);

            return Ok();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateRatingDTO updateRatingDTO)
        {
            var ratingToUpdate = await _ratingRepository.GetByIdAsync(id);

            if (ratingToUpdate == null) return BadRequest("No rating found");

            ratingToUpdate.UpdateRating(updateRatingDTO.Rating);

            await _ratingRepository.UpdateAsync(ratingToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var ratingToDelete = await _ratingRepository.GetByIdAsync(id);

            if (ratingToDelete == null) return BadRequest("No rating found");

            await _ratingRepository.RemoveAsync(ratingToDelete.Id);

            return NoContent();
        }

        private Func<RatingEntity, bool> BuildRatingsFilter(string demographicType)
        {
            var ratingsFilter = default(Func<RatingEntity, bool>);

            if (demographicType == "all")
            {
                ratingsFilter = ratings => true;
            }
            if (demographicType == "male")
            {
                ratingsFilter = ratings => ratings.Rater.Gender == "m";
            }
            if (demographicType == "female")
            {
                ratingsFilter = ratings => ratings.Rater.Gender == "f";
            }
            if (demographicType == "aged_under_18")
            {
                ratingsFilter = ratings => ratings.Rater.Age < 18;
            }
            if (demographicType == "aged_18_29")
            {
                ratingsFilter = ratings => (18 <= ratings.Rater.Age) && (ratings.Rater.Age <= 29);
            }
            if (demographicType == "aged_30_44")
            {
                ratingsFilter = ratings => (30 <= ratings.Rater.Age) && (ratings.Rater.Age <= 44);
            }
            if (demographicType == "aged_45_plus")
            {
                ratingsFilter = ratings => (45 <= ratings.Rater.Age);
            }

            return ratingsFilter;
        }
    }
}
