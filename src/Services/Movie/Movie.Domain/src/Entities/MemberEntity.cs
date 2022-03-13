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
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateRole(string role)
        {
            Role = role;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}