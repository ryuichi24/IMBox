using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IMBox.Shared.Infrastructure.Helpers.Hash
{
    public class HashService : IHashHelper
    {
        public (byte[] hash, byte[] salt) CreateHash(string plainValue)
        {
            if (string.IsNullOrWhiteSpace(plainValue))
                throw new ArgumentException("Value cannot be empty or whitespace.", nameof(plainValue));

            using var hmac = new HMACSHA512();

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainValue));
            var hashSalt = hmac.Key;

            return (hash, hashSalt);
        }

        public bool VerifyHash(string plainValue, byte[] hash, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(plainValue))
                throw new ArgumentException(
                    "Value cannot be empty or whitespace.", nameof(plainValue));

            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainValue));
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}