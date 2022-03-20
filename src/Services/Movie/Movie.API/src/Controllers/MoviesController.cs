using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Movie.API.DTOs;
using IMBox.Services.Movie.Domain.Entities;
using IMBox.Services.Movie.Domain.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public MoviesController(IMovieRepository movieRepository, IMemberRepository memberRepository, IPublishEndpoint publishEndpoint)
        {
            _movieRepository = movieRepository;
            _memberRepository = memberRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            var movies = await _movieRepository.GetAllAsync();

            var movieDTOs = await Task.WhenAll(movies.Select(async (movie) =>
            {
                var members = await _memberRepository.GetByMemberIdsAsync(movie.MemberIds);
                return movie.ToDTO(members);
            }));

            return Ok(movieDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return NotFound();

            var members = await _memberRepository.GetByMemberIdsAsync(movie.MemberIds);
            return Ok(movie.ToDTO(members));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDTO))]
        public async Task<IActionResult> CreateAsync(CreateMovieDTO createMovieDTO)
        {
            var movie = new MovieEntity
            {
                Title = createMovieDTO.Title,
                Description = createMovieDTO.Description,
                MainPosterUrl = createMovieDTO.MainPosterUrl,
                MainTrailerUrl = createMovieDTO.MainTrailerUrl,
                OtherPostUrls = createMovieDTO.OtherPostUrls,
                OtherTrailerUrls = createMovieDTO.OtherTrailerUrls,
                MemberIds = createMovieDTO.MemberIds
            };

            await _movieRepository.CreateAsync(movie);

            var members = await _memberRepository.GetByMemberIdsAsync(movie.MemberIds);

            var movieDTO = movie.ToDTO(members);

            await _publishEndpoint.Publish(new MovieCreatedIntegrationEvent
            {
                MovieId = movieDTO.Id,
                MovieTitle = movieDTO.Title,
                MovieDescription = movieDTO.Description,
                MemberIds = movieDTO.Members.Select(member => member.Id).ToList()
            });

            // https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtAction(nameof(GetByIdAsync), new { id = movie.Id }, movieDTO);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateMovieDTO updateMovieDTO)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);

            if (existingMovie == null) return BadRequest("No movie found");

            existingMovie
                .UpdateTitle(updateMovieDTO.Title)
                .UpdateDescription(updateMovieDTO.Description)
                .UpdateMainPosterUrl(updateMovieDTO.MainTrailerUrl)
                .UpdateMainTrailerUrl(updateMovieDTO.MainPosterUrl)
                .UpdateOtherPostUrls(updateMovieDTO.OtherPostUrls)
                .UpdateOtherTrailerUrls(updateMovieDTO.OtherTrailerUrls)
                .UpdateMemberIds(updateMovieDTO.MemberIds);

            await _movieRepository.UpdateAsync(existingMovie);

            await _publishEndpoint.Publish(new MovieUpdatedIntegrationEvent
            {
                MovieId = existingMovie.Id,
                MovieTitle = existingMovie.Title,
                MovieDescription = existingMovie.Description,
                MemberIds = existingMovie.MemberIds,
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var movieToDelete = await _movieRepository.GetByIdAsync(id);

            if (movieToDelete == null) return BadRequest("No movie found");

            await _movieRepository.RemoveAsync(movieToDelete.Id);

            await _publishEndpoint.Publish(new MovieDeletedIntegrationEvent
            {
                MovieId = movieToDelete.Id
            });

            return NoContent();
        }
    }
}