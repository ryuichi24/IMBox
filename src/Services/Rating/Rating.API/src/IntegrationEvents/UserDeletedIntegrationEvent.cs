using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record UserDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
    }
}
