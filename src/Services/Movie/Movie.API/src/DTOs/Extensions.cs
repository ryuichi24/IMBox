using System.Collections.Generic;
using System.Linq;
using IMBox.Services.Movie.Domain.Entities;

namespace IMBox.Services.Movie.API.DTOs
{
    public static class Extensions
    {
        public static MovieDTO ToDTO(this MovieEntity movieEntity, IEnumerable<MemberEntity> members)
        {
            var memberDTOs = members.Select(member => new MemberDTO
            {
                Id = member.Id,
                Name = member.Name,
                Role = member.Role
            });

            return new MovieDTO
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                Description = movieEntity.Description,
                MainPosterUrl = movieEntity.MainPosterUrl,
                MainTrailerUrl = movieEntity.MainTrailerUrl,
                OtherPostUrls = movieEntity.OtherPostUrls,
                OtherTrailerUrls = movieEntity.OtherTrailerUrls,
                CommentCount = movieEntity.CommentCount,
                Members = memberDTOs,
                Directors = memberDTOs.Where(memberItem => memberItem.Role == "director"),
                Writers = memberDTOs.Where(memberItem => memberItem.Role == "writer"),
                Casts = memberDTOs.Where(memberItem => memberItem.Role == "cast"),
            };
        }
    }
}