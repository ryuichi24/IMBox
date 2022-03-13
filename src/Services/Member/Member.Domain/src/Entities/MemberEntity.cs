using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Member.Domain.Entities
{
    public class MemberEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }

        public MemberEntity UpdateName(string newName)
        {
            if (String.IsNullOrEmpty(newName)) return this;
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateDescription(string description)
        {
            if (String.IsNullOrEmpty(description)) return this;
            Description = description;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateRole(string role)
        {
            if (String.IsNullOrEmpty(role)) return this;
            Role = role;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateBirthDate(DateTime birthDate)
        {
            if (birthDate == default(DateTime)) return this;
            BirthDate = birthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}