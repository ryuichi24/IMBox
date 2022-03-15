using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Movie.Domain.Entities;
using IMBox.Services.Movie.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IMBox.Services.Movie.API.Consumers
{
    public class MemberUpdatedIntegrationEventConsumer : IConsumer<MemberUpdatedIntegrationEvent>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<MemberUpdatedIntegrationEventConsumer> _logger;

        public MemberUpdatedIntegrationEventConsumer(ILogger<MemberUpdatedIntegrationEventConsumer> logger, IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MemberUpdatedIntegrationEvent> context)
        {
            var message = context.Message;

            _logger.LogDebug($"Message: {message.Id} has been consummed by {nameof(MemberUpdatedIntegrationEventConsumer)}");

            var existingMember = await _memberRepository.GetByIdAsync(message.MemberId);

            if (existingMember == null)
            {
                var newMember = new MemberEntity
                {
                    Id = message.MemberId,
                    Name = message.MemberName,
                    Role = message.MemberRole
                };

                await _memberRepository.CreateAsync(newMember);
                return;
            }

            existingMember
                .UpdateName(message.MemberName)
                .UpdateRole(message.MemberRole);

            await _memberRepository.UpdateAsync(existingMember);
        }
    }
}