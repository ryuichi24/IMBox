using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Movie.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Movie.API.Consumers
{
    public class MemberDeletedIntegrationEventConsumer : IConsumer<MemberDeletedIntegrationEvent>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<MemberDeletedIntegrationEventConsumer> _logger;

        public MemberDeletedIntegrationEventConsumer(ILogger<MemberDeletedIntegrationEventConsumer> logger, IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MemberDeletedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MemberDeletedIntegrationEventConsumer)}");

            var existingMember = await _memberRepository.GetByIdAsync(message.MemberId);

            if (existingMember != null) return;

            await _memberRepository.RemoveAsync(existingMember.Id);
        }
    }
}