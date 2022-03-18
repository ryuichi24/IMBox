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
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserCreatedIntegrationEventConsumer> _logger;

        public UserCreatedIntegrationEventConsumer(IUserRepository userRepository, ILogger<UserCreatedIntegrationEventConsumer> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(UserCreatedIntegrationEventConsumer)}");

            var existingUser = await _userRepository.GetByIdAsync(message.UserId);

            if (existingUser != null) return;

            var newUser = new UserEntity
            {
                Id = message.UserId,
                Username = message.UserUsername,
                Gender = message.UserGender,
                BirthDate = message.UserBirthDate,
                Continent = message.UserContinent
            };

            await _userRepository.CreateAsync(newUser);
        }
    }
}