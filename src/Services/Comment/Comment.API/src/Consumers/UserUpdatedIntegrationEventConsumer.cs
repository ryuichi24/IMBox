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
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserUpdatedIntegrationEventConsumer> _logger;

        public UserUpdatedIntegrationEventConsumer(IUserRepository userRepository, ILogger<UserUpdatedIntegrationEventConsumer> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserUpdatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserUpdatedIntegrationEventConsumer)}");

            var existingUser = await _userRepository.GetByIdAsync(message.UserId);

            if (existingUser == null)
            {
                var newUser = new UserEntity
                {
                    Id = message.UserId,
                    Username = message.UserUsername,
                    Gender = message.UserGender,
                    BirthDate = message.UserBirthDate,
                    Continent = message.UserContinent
                };

                await _userRepository.CreateAsync(newUser);
                return;
            }

            existingUser
                .UpdateUsername(message.UserUsername)
                .UpdateGender(message.UserGender)
                .UpdateBirthDate(message.UserBirthDate)
                .UpdateContinent(message.UserContinent);

            await _userRepository.UpdateAsync(existingUser);
        }
    }
}