using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Movie.API.Consumers
{
    public class MemberDeletedIntegrationEventConsumer : IConsumer<MemberDeletedIntegrationEvent>
    {
        private readonly ILogger<MemberDeletedIntegrationEventConsumer> _logger;

        public MemberDeletedIntegrationEventConsumer(ILogger<MemberDeletedIntegrationEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MemberDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MemberDeletedIntegrationEventConsumer)}");
        }
    }
}