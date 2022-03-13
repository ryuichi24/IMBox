using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record MemberUpdatedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; init; }
        public string MemberName { get; init; }
        public string MemberRole { get; set; }
    }
}
