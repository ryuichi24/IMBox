using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record CommentDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid CommentId { get; init; }
        public Guid MovieId { get; init; }

    }
}
