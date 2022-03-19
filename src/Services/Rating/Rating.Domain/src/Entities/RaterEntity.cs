using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Rating.Domain.Entities
{
    public class RaterEntity : Entity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - BirthDate.Year;
                if (BirthDate.AddYears(age) > now)
                    age--;
                return age;
            }
        }

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

        public RaterEntity UpdateCountry(string newCountry)
        {
            if (String.IsNullOrWhiteSpace(newCountry)) return this;
            Country = newCountry;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}
