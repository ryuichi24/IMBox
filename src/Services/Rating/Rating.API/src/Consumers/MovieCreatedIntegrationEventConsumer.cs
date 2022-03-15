using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Rating.API.Consumers
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
        }
    }
}