using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Member.Domain.Entities;
using IMBox.Services.Member.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Member.API.Consumers
{
    public class MovieCreatedIntegrationEventConsumer : IConsumer<MovieCreatedIntegrationEvent>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieCreatedIntegrationEventConsumer> _logger;

        public MovieCreatedIntegrationEventConsumer(IMovieRepository movieRepository, ILogger<MovieCreatedIntegrationEventConsumer> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MovieCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MovieCreatedIntegrationEventConsumer)}");

            var existingMovie = await _movieRepository.GetByIdAsync(message.MovieId);

            if (existingMovie != null) return;

            var newMovie = new MovieEntity
            {
                Id = message.MovieId,
                Title = message.MovieTitle,
                Description = message.MovieDescription,
                MemberIds = message.MemberIds
            };

            await _movieRepository.CreateAsync(newMovie);
        }
    }
}