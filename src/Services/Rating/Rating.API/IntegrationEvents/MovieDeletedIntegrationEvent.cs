using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record MovieDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid MovieId { get; init; }
    }
}
