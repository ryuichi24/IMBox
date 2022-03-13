using System;
using System.Threading.Tasks;
using IMBox.Services.Movie.API.DTOs;
using IMBox.Services.Movie.Domain.Entities;
using IMBox.Services.Movie.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET /movies/{id}
        [HttpGet("{id}")]
        [ProducesResponseType((int)200, Type = typeof(MovieDTO))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie.ToDTO());
        }

        // POST /movies
        [HttpPost()]
        [ProducesResponseType((int)201, Type = typeof(MovieDTO))]
        public async Task<IActionResult> Create(CreateMovieDTO createMovieDTO)
        {
            var movie = new MovieEntity
            {
                Title = createMovieDTO.Title,
                Description = createMovieDTO.Description,
            };

            await _movieRepository.CreateAsync(movie);

            // https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie.ToDTO());
        }

        // PUT /movies/{id}
        [HttpPut("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public async Task<IActionResult> Update(Guid id, UpdateMovieDTO updateMovieDTO)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);

            if (existingMovie == null) return BadRequest("No movie found");

            existingMovie
                .UpdateTitle(updateMovieDTO.Title)
                .UpdateDescription(updateMovieDTO.Description);

            await _movieRepository.UpdateAsync(existingMovie);

            return NoContent();
        }

        // DELETE /movies/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movieToDelete = await _movieRepository.GetByIdAsync(id);

            if (movieToDelete == null) return BadRequest("No movie found");

            await _movieRepository.RemoveAsync(movieToDelete.Id);

            return NoContent();
        }
    }
}