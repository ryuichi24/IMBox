using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Rating.API.Consumers
{
    public class UserCreatedIntegrationEventConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly IRaterRepository _raterRepository;
        private readonly ILogger<UserCreatedIntegrationEventConsumer> _logger;

        public UserCreatedIntegrationEventConsumer(IRaterRepository raterRepository, ILogger<UserCreatedIntegrationEventConsumer> logger)
        {
            _raterRepository = raterRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserCreatedIntegrationEventConsumer)}");

            var existingRater = await _raterRepository.GetByIdAsync(message.UserId);

            if (existingRater != null) return;

            var newRater = new RaterEntity
            {
                Id = message.UserId,
                Name = message.UserUsername,
                Gender = message.UserGender,
                BirthDate = message.UserBirthDate,
                Country = message.UserCountry
            };

            await _raterRepository.CreateAsync(newRater);
        }
    }
}