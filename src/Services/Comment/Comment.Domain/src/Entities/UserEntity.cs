using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Comment.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Continent { get; set; }
        public bool IsActive { get; set; } = false;


        public UserEntity UpdateUsername(string newUsername)
        {
            if (String.IsNullOrWhiteSpace(newUsername)) return this;
            Username = newUsername;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }


        public UserEntity UpdateBirthDate(DateTime newBirthDate)
        {
            if (newBirthDate == default(DateTime)) return this;
            BirthDate = newBirthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity UpdateGender(string newGender)
        {
            if (String.IsNullOrWhiteSpace(newGender)) return this;
            Gender = newGender;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity UpdateContinent(string newContinent)
        {
            if (String.IsNullOrWhiteSpace(newContinent)) return this;
            Continent = newContinent;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity Activate()
        {
            IsActive = true;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

    }
}