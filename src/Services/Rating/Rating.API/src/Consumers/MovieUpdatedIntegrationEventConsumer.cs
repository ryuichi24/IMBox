using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Rating.API.Consumers
{
    public class MovieUpdatedIntegrationEventConsumer : IConsumer<MovieUpdatedIntegrationEvent>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieUpdatedIntegrationEventConsumer> _logger;

        public MovieUpdatedIntegrationEventConsumer(IMovieRepository movieRepository, ILogger<MovieUpdatedIntegrationEventConsumer> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MovieUpdatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MovieUpdatedIntegrationEventConsumer)}");

            var existingMovie = await _movieRepository.GetByIdAsync(message.MovieId);

            if (existingMovie == null)
            {
                var newMovie = new MovieEntity
                {
                    Id = message.MovieId,
                    Title = message.MovieTitle,
                    Description = message.MovieDescription
                };

                await _movieRepository.CreateAsync(newMovie);
                return;
            }

            existingMovie
                .UpdateTitle(message.MovieTitle)
                .UpdateDescription(message.MovieDescription);

            await _movieRepository.UpdateAsync(existingMovie);
        }
    }
}