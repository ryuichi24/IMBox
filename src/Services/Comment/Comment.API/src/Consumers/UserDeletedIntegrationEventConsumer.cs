using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Comment.API.Consumers
{
    public class UserDeletedIntegrationEventConsumer : IConsumer<UserDeletedIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserDeletedIntegrationEventConsumer> _logger;

        public UserDeletedIntegrationEventConsumer(IUserRepository userRepository, ILogger<UserDeletedIntegrationEventConsumer> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserDeletedIntegrationEventConsumer)}");

            var existingUser = await _userRepository.GetByIdAsync(message.UserId);

            if(existingUser == null) return;

            await _userRepository.RemoveAsync(existingUser.Id);
        }
    }
}