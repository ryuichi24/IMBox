using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Movie.API.Consumers
{
    public class MemberUpdatedIntegrationEventConsumer : IConsumer<MemberUpdatedIntegrationEvent>
    {
        private readonly ILogger<MemberUpdatedIntegrationEventConsumer> _logger;

        public MemberUpdatedIntegrationEventConsumer(ILogger<MemberUpdatedIntegrationEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MemberUpdatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MemberUpdatedIntegrationEventConsumer)}");
        }
    }
}