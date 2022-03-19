using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Rating.API.Consumers
{
    public class UserDeletedIntegrationEventConsumer : IConsumer<UserDeletedIntegrationEvent>
    {
        private readonly IRaterRepository _raterRepository;
        private readonly ILogger<UserDeletedIntegrationEventConsumer> _logger;

        public UserDeletedIntegrationEventConsumer(IRaterRepository raterRepository, ILogger<UserDeletedIntegrationEventConsumer> logger)
        {
            _raterRepository = raterRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserDeletedIntegrationEventConsumer)}");

            var existingRater = await _raterRepository.GetByIdAsync(message.UserId);

            if(existingRater == null) return;

            await _raterRepository.RemoveAsync(existingRater.Id);
        }
    }
}