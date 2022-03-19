using System;
using System.Collections.Generic;
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
        public string Gender { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; } = false;
        public string EmailConfirmToken { get; set; }
        public IEnumerable<string> Roles { get; set; }

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

        public UserEntity UpdateGender(string newGender)
        {
            if (String.IsNullOrWhiteSpace(newGender)) return this;
            Gender = newGender;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public UserEntity UpdateCountry(string newCountry)
        {
            if (String.IsNullOrWhiteSpace(newCountry)) return this;
            Country = newCountry;
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

        public UserEntity UpdateRoles(IEnumerable<string> roles)
        {
            if (roles == default(IEnumerable<string>)) return this;
            Roles = roles;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

    }
}