using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Member.Domain.Entities
{
    public class MemberEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadshotUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }


        public MemberEntity UpdateName(string newName)
        {
            if (String.IsNullOrEmpty(newName)) return this;
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateDescription(string newDescription)
        {
            if (String.IsNullOrEmpty(newDescription)) return this;
            Description = newDescription;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MemberEntity UpdateHeadshotUrl(string newHeadshotUrl)
        {
            if (String.IsNullOrEmpty(newHeadshotUrl)) return this;
            HeadshotUrl = newHeadshotUrl;
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

        public MemberEntity UpdateBirthDate(DateTime newBirthDate)
        {
            if (newBirthDate == default(DateTime)) return this;
            BirthDate = newBirthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}