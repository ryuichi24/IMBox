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
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateRole(string role)
        {
            Role = role;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}