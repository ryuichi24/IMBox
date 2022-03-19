using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Rating.Domain.Entities
{
    public class RaterEntity : Entity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Continent { get; set; }


        public RaterEntity UpdateName(string newName)
        {
            if (String.IsNullOrWhiteSpace(newName)) return this;
            Name = newName;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }


        public RaterEntity UpdateBirthDate(DateTime newBirthDate)
        {
            if (newBirthDate == default(DateTime)) return this;
            BirthDate = newBirthDate;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public RaterEntity UpdateGender(string newGender)
        {
            if (String.IsNullOrWhiteSpace(newGender)) return this;
            Gender = newGender;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public RaterEntity UpdateContinent(string newContinent)
        {
            if (String.IsNullOrWhiteSpace(newContinent)) return this;
            Continent = newContinent;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}