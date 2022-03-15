using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Member.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Member.API.Consumers
{
    public class MovieDeletedIntegrationEventConsumer : IConsumer<MovieDeletedIntegrationEvent>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieDeletedIntegrationEventConsumer> _logger;

        public MovieDeletedIntegrationEventConsumer(IMovieRepository movieRepository, ILogger<MovieDeletedIntegrationEventConsumer> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MovieDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MovieDeletedIntegrationEventConsumer)}");

            var existingMovie = await _movieRepository.GetByIdAsync(message.MovieId);

            if (existingMovie == null) return;

            await _movieRepository.RemoveAsync(existingMovie.Id);
        }
    }
}