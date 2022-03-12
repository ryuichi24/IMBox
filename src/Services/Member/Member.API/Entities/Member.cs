using System;
using IMBox.Shared.Core.Entity;

namespace IMBox.Services.Member.API.Entities
{
    public class Member : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public Member UpdateName(string newName)
        {
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public Member UpdateDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public Member UpdateRole(string role)
        {
            Role = role;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public Member UpdateBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}