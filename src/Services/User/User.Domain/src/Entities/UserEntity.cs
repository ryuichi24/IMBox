using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.User.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordHashSalt { get; set; }
        public DateTime BirthDate { get; set; }
        public Char Gender { get; set; }
        public string Continent { get; set; }
        public bool IsActive { get; set; } = false;
        public string EmailConfirmToken { get; set; }

        public UserEntity UpdateEmail(string newEmail)
        {
            if (String.IsNullOrWhiteSpace(newEmail)) return this;
            Email = newEmail;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity UpdateUsername(string newUsername)
        {
            if (String.IsNullOrWhiteSpace(newUsername)) return this;
            Username = newUsername;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity UpdatePasswordHash(Byte[] newPasswordHash)
        {
            if (newPasswordHash == default(Byte[])) return this;
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity UpdatePasswordHashSalt(Byte[] newPasswordHashSalt)
        {
            if (newPasswordHashSalt == default(Byte[])) return this;
            PasswordHashSalt = newPasswordHashSalt;
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

        public UserEntity UpdateGender(Char newGender)
        {
            if (newGender == default(Char)) return this;
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

        public UserEntity UpdateEmailConfirmToken(string newEmailConfirmToken)
        {
            if (String.IsNullOrWhiteSpace(newEmailConfirmToken)) return this;
            EmailConfirmToken = newEmailConfirmToken;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity RevokeEmailConfirmToken()
        {
            EmailConfirmToken = null;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}