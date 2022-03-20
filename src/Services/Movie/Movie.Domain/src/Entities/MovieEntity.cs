using System;
using System.Collections.Generic;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.Movie.Domain.Entities
{
    public class MovieEntity : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainPosterUrl { get; set; }
        public string MainTrailerUrl { get; set; }
        public List<string> OtherPostUrls { get; set; }
        public List<string> OtherTrailerUrls { get; set; }
        public int CommentCount { get; set; } = 0;
        public List<Guid> MemberIds { get; set; }

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

        public MovieEntity UpdateMemberIds(List<Guid> memberIds)
        {
            if (memberIds == default(List<Guid>)) return this;
            MemberIds = memberIds;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity UpdateMainPosterUrl(string newMainPosterUrl)
        {
            if (String.IsNullOrEmpty(newMainPosterUrl)) return this;
            MainPosterUrl = newMainPosterUrl;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity UpdateMainTrailerUrl(string newMainTrailerUrl)
        {
            if (String.IsNullOrEmpty(newMainTrailerUrl)) return this;
            MainTrailerUrl = newMainTrailerUrl;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity UpdateOtherPostUrls(List<string> newOtherPostUrls)
        {
            if (newOtherPostUrls == default(List<string>)) return this;
            OtherPostUrls = newOtherPostUrls;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity UpdateOtherTrailerUrls(List<string> newOtherTrailerUrls)
        {
            if (newOtherTrailerUrls == default(List<string>)) return this;
            OtherTrailerUrls = newOtherTrailerUrls;
            UpdatedAt = DateTimeOffset.UtcNow;
            return this;
        }

        public MovieEntity IncrementCommentCount()
        {
            CommentCount++;
            return this;
        }

        public MovieEntity DecrementCommentCount()
        {
            CommentCount--;
            return this;
        }
    }
}
