using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Movie.Domain.Entities
{
    public class MovieEntity : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public MovieEntity UpdateTitle(string newTitle)
        {
            Title = newTitle;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity UpdateDescription(string newDescription)
        {
            Description = newDescription;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}