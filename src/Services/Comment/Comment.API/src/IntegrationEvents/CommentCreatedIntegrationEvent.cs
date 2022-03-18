using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record CommentCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid CommentId { get; init; }
        public string Text { get; init; }
        public Guid MovieId { get; init; }
        public Guid CommenterId { get; init; }
    }
}
