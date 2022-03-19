using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Rating.Domain.Entities
{
    public class MovieEntity : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public MovieEntity UpdateTitle(string newTitle)
        {
            if (String.IsNullOrEmpty(newTitle)) return this;
            Title = newTitle;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity UpdateDescription(string newDescription)
        {
            if (String.IsNullOrEmpty(newDescription)) return this;
            Description = newDescription;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}