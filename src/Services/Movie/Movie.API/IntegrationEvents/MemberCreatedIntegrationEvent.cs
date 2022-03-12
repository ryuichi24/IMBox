using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record MemberCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; init; }
        public string MemberName { get; init; }
    }
}
