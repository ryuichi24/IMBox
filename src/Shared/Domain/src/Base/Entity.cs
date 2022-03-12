using System;

namespace IMBox.Shared.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}