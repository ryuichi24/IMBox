using System;
using System.Collections.Generic;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record MovieCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid MovieId { get; init; }
        public string MovieTitle { get; init; }
        public string MovieDescription { get; set; }
        public List<Guid> MemberIds { get; set; }
    }
}
