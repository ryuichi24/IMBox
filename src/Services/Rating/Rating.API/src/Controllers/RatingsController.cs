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
        public RatingsController(IRatingRepository ratingRepository, IRaterRepository raterRepository)
        {
            _ratingRepository = ratingRepository;
            _raterRepository = raterRepository;
        }

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRatingsResponseDTO))]
        public async Task<IActionResult> GetAsync([FromQuery] GetRatingsDTO getRatingDTO)
        {
            if (getRatingDTO.MovieId == null) return BadRequest("Movie Id is not provided.");

            var ratings = await _ratingRepository.GetAllByMovieId(getRatingDTO.MovieId);
            var raters = await _raterRepository.GetAllAsync();

            var ratingsWithRaters = ratings.Select(rating =>
            {
                rating.Rater = raters.Find(rater => rater.Id == rating.RaterId);
                return rating;
            });

            Func<RatingEntity, bool> ratingsFilter = ratings => true;

            if (getRatingDTO.DemographicType == "all")
            {
                ratingsFilter = ratings => true;
            }
            if (getRatingDTO.DemographicType == "male")
            {
                ratingsFilter = ratings => ratings.Rater.Gender == "m";
            }
            if (getRatingDTO.DemographicType == "female")
            {
                ratingsFilter = ratings => ratings.Rater.Gender == "f";
            }
            if (getRatingDTO.DemographicType == "aged_under_18")
            {
                ratingsFilter = ratings => ratings.Rater.Age < 18;
            }
            if (getRatingDTO.DemographicType == "aged_18_29")
            {
                ratingsFilter = ratings => (18 <= ratings.Rater.Age) && (ratings.Rater.Age <= 29);
            }
            if (getRatingDTO.DemographicType == "aged_30_44")
            {
                ratingsFilter = ratings => (30 <= ratings.Rater.Age) && (ratings.Rater.Age <= 44);
            }
            if (getRatingDTO.DemographicType == "aged_45_plus")
            {
                ratingsFilter = ratings => (45 <= ratings.Rater.Age);
            }

            var filteredRatings = ratingsWithRaters.Where(ratingsFilter);

            return Ok(filteredRatings);
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
    }
}
