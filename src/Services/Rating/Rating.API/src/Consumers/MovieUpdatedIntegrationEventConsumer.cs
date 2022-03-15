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
        }
    }
}