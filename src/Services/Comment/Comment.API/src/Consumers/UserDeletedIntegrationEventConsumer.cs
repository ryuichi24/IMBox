using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Comment.API.Consumers
{
    public class UserDeletedIntegrationEventConsumer : IConsumer<UserDeletedIntegrationEvent>
    {
        private readonly ICommenterRepository _commenterRepository;
        private readonly ILogger<UserDeletedIntegrationEventConsumer> _logger;

        public UserDeletedIntegrationEventConsumer(ICommenterRepository commenterRepository, ILogger<UserDeletedIntegrationEventConsumer> logger)
        {
            _commenterRepository = commenterRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserDeletedIntegrationEventConsumer)}");

            var existingCommenter = await _commenterRepository.GetByIdAsync(message.UserId);

            if(existingCommenter == null) return;

            await _commenterRepository.RemoveAsync(existingCommenter.Id);
        }
    }
}