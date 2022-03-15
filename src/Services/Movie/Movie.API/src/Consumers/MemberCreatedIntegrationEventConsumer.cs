using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Movie.Domain.Entities;
using IMBox.Services.Movie.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Movie.API.Consumers
{
    public class MemberCreatedIntegrationEventConsumer : IConsumer<MemberCreatedIntegrationEvent>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<MemberCreatedIntegrationEventConsumer> _logger;

        public MemberCreatedIntegrationEventConsumer(ILogger<MemberCreatedIntegrationEventConsumer> logger, IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MemberCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MemberCreatedIntegrationEventConsumer)}");

            var existingMember = await _memberRepository.GetByIdAsync(message.MemberId);

            if (existingMember != null) return;

            var newMember = new MemberEntity
            {
                Id = message.MemberId,
                Name = message.MemberName,
                Role = message.MemberRole
            };

            await _memberRepository.CreateAsync(newMember);
        }
    }
}