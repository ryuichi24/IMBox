using System;
using IMBox.Shared.Infrastructure.EventBus;

namespace IMBox.Services.IntegrationEvents
{
    public record UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string UserUsername { get; init; }
        public DateTime UserBirthDate { get; init; }
        public string UserGender { get; init; }
        public string UserContinent { get; init; }
    }
}
