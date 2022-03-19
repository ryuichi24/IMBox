using System;
using System.Collections.Generic;
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
        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
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