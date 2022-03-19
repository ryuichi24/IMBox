using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Comment.API.Consumers
{
    public class UserCreatedIntegrationEventConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly ICommenterRepository _commenterRepository;
        private readonly ILogger<UserCreatedIntegrationEventConsumer> _logger;

        public UserCreatedIntegrationEventConsumer(ICommenterRepository commenterRepository, ILogger<UserCreatedIntegrationEventConsumer> logger)
        {
            _commenterRepository = commenterRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserCreatedIntegrationEventConsumer)}");

            var existingCommenter = await _commenterRepository.GetByIdAsync(message.UserId);

            if (existingCommenter != null) return;

            var newCommenter = new CommenterEntity
            {
                Id = message.UserId,
                Name = message.UserUsername,
                Gender = message.UserGender,
                BirthDate = message.UserBirthDate,
                Country = message.UserCountry
            };

            await _commenterRepository.CreateAsync(newCommenter);
        }
    }
}