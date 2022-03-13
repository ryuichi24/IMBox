using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Movie.Domain.Entities
{
    public class MemberEntity : Entity
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public MemberEntity UpdateName(string newName)
        {
            if (String.IsNullOrEmpty(newName)) return this;
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateRole(string newRole)
        {
            if (String.IsNullOrEmpty(newRole)) return this;
            Role = newRole;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}