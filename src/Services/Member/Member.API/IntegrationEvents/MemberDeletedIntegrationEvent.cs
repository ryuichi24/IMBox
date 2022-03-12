using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.Member.API.IntegrationEvents
{
    public record MemberDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; init; }
    }
}
