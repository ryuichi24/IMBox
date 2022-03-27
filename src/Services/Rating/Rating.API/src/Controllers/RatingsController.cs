using System;
using System.Collections.Generic;
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
    [Route("api/[controller]")]
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
        [Route("/api/movies/{movieId}/ratings")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RatingAnalysisDTO))]
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
            var totalFilteredRatingCount = filteredRatings.Count;

            var ratingOneItems = filteredRatings.Where(ratingItem => ratingItem.Rating == 1).ToList();
            var totalRatingOneItemCount = ratingOneItems.Count;
            var ratingOnePercent = totalFilteredRatingCount == 0 ? 0 : ((decimal)totalRatingOneItemCount / (decimal)totalFilteredRatingCount) * 100;

            var ratingTwoItems = filteredRatings.Where(ratingItem => ratingItem.Rating == 2).ToList();
            var totalRatingTwoItemCount = ratingTwoItems.Count;
            var ratingTwoPercent = totalFilteredRatingCount == 0 ? 0 : ((decimal)totalRatingTwoItemCount / (decimal)totalFilteredRatingCount) * 100;

            var ratingThreeItems = filteredRatings.Where(ratingItem => ratingItem.Rating == 3).ToList();
            var totalRatingThreeItemCount = ratingThreeItems.Count;
            var ratingThreePercent = totalFilteredRatingCount == 0 ? 0 : ((decimal)totalRatingThreeItemCount / (decimal)totalFilteredRatingCount) * 100;

            var ratingFourItems = filteredRatings.Where(ratingItem => ratingItem.Rating == 4).ToList();
            var totalRatingFourItemCount = ratingFourItems.Count;
            var ratingFourPercent = totalFilteredRatingCount == 0 ? 0 : ((decimal)totalRatingFourItemCount / (decimal)totalFilteredRatingCount) * 100;

            var ratingFiveItems = filteredRatings.Where(ratingItem => ratingItem.Rating == 5).ToList();
            var totalRatingFiveItemCount = ratingFiveItems.Count;
            var ratingFivePercent = totalFilteredRatingCount == 0 ? 0 : ((decimal)totalRatingFiveItemCount / (decimal)totalFilteredRatingCount) * 100;

            var ratingItems = new List<RatingItem>
                {
                    new RatingItem
                    {
                        Rating = 1,
                        RatingVoteCount = totalRatingOneItemCount,
                        Percent = $"{ratingOnePercent.ToString("0")}%"
                    },
                    new RatingItem
                    {
                        Rating = 2,
                        RatingVoteCount = totalRatingTwoItemCount,
                        Percent = $"{ratingTwoPercent.ToString("0")}%"
                    },
                    new RatingItem
                    {
                        Rating = 3,
                        RatingVoteCount = totalRatingThreeItemCount,
                        Percent = $"{ratingThreePercent.ToString("0")}%"
                    },
                    new RatingItem
                    {
                        Rating = 4,
                        RatingVoteCount = totalRatingFourItemCount,
                        Percent = $"{ratingFourPercent.ToString("0")}%"
                    },
                    new RatingItem
                    {
                        Rating = 5,
                        RatingVoteCount = totalRatingFiveItemCount,
                        Percent = $"{ratingFivePercent.ToString("0")}%"
                    },
                };

            var averageRating = totalFilteredRatingCount == 0 ? 0 : (decimal)(ratingItems.Select(ratingItem => ratingItem.RatingVoteCount * ratingItem.Rating).Sum()) / (decimal)totalFilteredRatingCount;

            var dto = new RatingAnalysisDTO
            {
                Movie = movie.ToDTO(),
                DemographicType = demographicType,
                AverageRating = averageRating,
                TotalRatingVoteCount = totalFilteredRatingCount,
                Ratings = ratingItems
            };

            return Ok(dto);
        }

        [AllowAnonymous]
        [Route("/api/raters/{raterId}/ratings")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserRatingsDTO))]
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


            return Ok(new UserRatingsDTO
            {
                Rater = rater.ToDTO(),
                Ratings = ratingDTOs
            });
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> CreateAsync(CreateRatingDTO createRatingDTO)
        {
            var existingRating = await _ratingRepository.GetByRaterIdAndMovieId(User.SubjectId().ToGuid(), createRatingDTO.MovieId);
            if (existingRating != null) return BadRequest("The user already rated the movie.");

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
            if (demographicType == "males")
            {
                ratingsFilter = ratings => ratings.Rater.Gender == "m";
            }
            if (demographicType == "females")
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
            if (demographicType == "males_aged_under_18")
            {
                ratingsFilter = ratings => (ratings.Rater.Age < 18) && ratings.Rater.Gender == "m";
            }
            if (demographicType == "males_aged_18_29")
            {
                ratingsFilter = ratings => (18 <= ratings.Rater.Age) && (ratings.Rater.Age <= 29) && ratings.Rater.Gender == "m";
            }
            if (demographicType == "males_aged_30_44")
            {
                ratingsFilter = ratings => (30 <= ratings.Rater.Age) && (ratings.Rater.Age <= 44) && ratings.Rater.Gender == "m";
            }
            if (demographicType == "males_aged_45_plus")
            {
                ratingsFilter = ratings => (45 <= ratings.Rater.Age) && ratings.Rater.Gender == "m";
            }
            if (demographicType == "females_aged_under_18")
            {
                ratingsFilter = ratings => ratings.Rater.Age < 18 && (ratings.Rater.Gender == "f");
            }
            if (demographicType == "females_aged_18_29")
            {
                ratingsFilter = ratings => (18 <= ratings.Rater.Age) && (ratings.Rater.Age <= 29) && (ratings.Rater.Gender == "f");
            }
            if (demographicType == "females_aged_30_44")
            {
                ratingsFilter = ratings => (30 <= ratings.Rater.Age) && (ratings.Rater.Age <= 44) && (ratings.Rater.Gender == "f");
            }
            if (demographicType == "females_aged_45_plus")
            {
                ratingsFilter = ratings => (45 <= ratings.Rater.Age) && (ratings.Rater.Gender == "f");
            }

            return ratingsFilter;
        }
    }
}
