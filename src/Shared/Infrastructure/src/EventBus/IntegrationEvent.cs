using System;

namespace IMBox.Shared.Infrastructure.EventBus
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            PublishedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private init; }

        public DateTimeOffset PublishedAt { get; private init; }
    }
}