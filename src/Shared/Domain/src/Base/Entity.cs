using System;

namespace IMBox.Shared.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    }
}