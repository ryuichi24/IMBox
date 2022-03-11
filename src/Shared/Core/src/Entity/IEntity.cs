using System;

namespace IMBox.Shared.Core.Entity
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}