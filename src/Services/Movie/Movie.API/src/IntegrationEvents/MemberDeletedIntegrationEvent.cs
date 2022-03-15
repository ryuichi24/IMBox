using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record MemberDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; init; }
    }
}
