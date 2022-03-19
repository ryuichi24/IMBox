using System.Threading.Tasks;
using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Rating.API.Consumers
{
    public class UserUpdatedIntegrationEventConsumer : IConsumer<UserUpdatedIntegrationEvent>
    {
        private readonly IRaterRepository _raterRepository;
        private readonly ILogger<UserUpdatedIntegrationEventConsumer> _logger;

        public UserUpdatedIntegrationEventConsumer(IRaterRepository raterRepository, ILogger<UserUpdatedIntegrationEventConsumer> logger)
        {
            _raterRepository = raterRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserUpdatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserUpdatedIntegrationEventConsumer)}");

            var existingRater = await _raterRepository.GetByIdAsync(message.UserId);

            if (existingRater == null)
            {
                var newRater = new RaterEntity
                {
                    Id = message.UserId,
                    Name = message.UserUsername,
                    Gender = message.UserGender,
                    BirthDate = message.UserBirthDate,
                    Country = message.UserCountry
                };

                await _raterRepository.CreateAsync(newRater);
                return;
            }

            existingRater
                .UpdateName(message.UserUsername)
                .UpdateGender(message.UserGender)
                .UpdateBirthDate(message.UserBirthDate)
                .UpdateCountry(message.UserCountry);

            await _raterRepository.UpdateAsync(existingRater);
        }
    }
}