using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Comment.Domain.Entities
{
    public class CommenterEntity : Entity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }


        public CommenterEntity UpdateName(string newName)
        {
            if (String.IsNullOrWhiteSpace(newName)) return this;
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }


        public CommenterEntity UpdateBirthDate(DateTime newBirthDate)
        {
            if (newBirthDate == default(DateTime)) return this;
            BirthDate = newBirthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public CommenterEntity UpdateGender(string newGender)
        {
            if (String.IsNullOrWhiteSpace(newGender)) return this;
            Gender = newGender;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public CommenterEntity UpdateCountry(string newCountry)
        {
            if (String.IsNullOrWhiteSpace(newCountry)) return this;
            Country = newCountry;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

    }
}