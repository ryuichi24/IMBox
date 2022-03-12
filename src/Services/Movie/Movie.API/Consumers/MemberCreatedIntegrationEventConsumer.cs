using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Movie.API.Consumers
{
    public class MemberCreatedIntegrationEventConsumer : IConsumer<MemberCreatedIntegrationEvent>
    {
        private readonly ILogger<MemberCreatedIntegrationEventConsumer> _logger;

        public MemberCreatedIntegrationEventConsumer(ILogger<MemberCreatedIntegrationEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MemberCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MemberCreatedIntegrationEventConsumer)}");
        }
    }
}