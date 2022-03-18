using System.Threading.Tasks;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Comment.API.Consumers
{
    public class UserUpdatedIntegrationEventConsumer : IConsumer<UserUpdatedIntegrationEvent>
    {
        private readonly ICommenterRepository _commenterRepository;
        private readonly ILogger<UserUpdatedIntegrationEventConsumer> _logger;

        public UserUpdatedIntegrationEventConsumer(ICommenterRepository commenterRepository, ILogger<UserUpdatedIntegrationEventConsumer> logger)
        {
            _commenterRepository = commenterRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserUpdatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserUpdatedIntegrationEventConsumer)}");

            var existingCommenter = await _commenterRepository.GetByIdAsync(message.UserId);

            if (existingCommenter == null)
            {
                var newCommenter = new CommenterEntity
                {
                    Id = message.UserId,
                    Name = message.UserUsername,
                    Gender = message.UserGender,
                    BirthDate = message.UserBirthDate,
                    Continent = message.UserContinent
                };

                await _commenterRepository.CreateAsync(newCommenter);
                return;
            }

            existingCommenter
                .UpdateName(message.UserUsername)
                .UpdateGender(message.UserGender)
                .UpdateBirthDate(message.UserBirthDate)
                .UpdateContinent(message.UserContinent);

            await _commenterRepository.UpdateAsync(existingCommenter);
        }
    }
}