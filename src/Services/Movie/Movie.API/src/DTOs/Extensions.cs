using System.Collections.Generic;
using System.Linq;
using IMBox.Services.Movie.Domain.Entities;

namespace IMBox.Services.Movie.API.DTOs
{
    public static class Extensions
    {
        public static MovieDTO ToDTO(this MovieEntity MovieEntity, IEnumerable<MemberEntity> members)
        {
            var memberDTOs = members.Select(member => new MemberDTO
            {
                Id = member.Id,
                Name = member.Name,
                Role = member.Role
            });

            return new MovieDTO
            {
                Id = MovieEntity.Id,
                Title = MovieEntity.Title,
                Description = MovieEntity.Description,
                CommentCount = MovieEntity.CommentCount,
                Members = memberDTOs
            };
        }
    }
}