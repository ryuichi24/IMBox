using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.Member.API.IntegrationEvents
{
    public record MemberCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; init; }
        public string MemberName { get; init; }
    }
}
