using System;
using System.Collections.Generic;
using System.Linq;
using IMBox.Services.Member.Domain.Entities;

namespace IMBox.Services.Member.API.DTOs
{
    public static class Extensions
    {
        public static MemberDTO ToDTO(this MemberEntity memberEntity, IEnumerable<MovieEntity> movies)
        {
            var movieDTOs = movies.Select(movie => new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description
            });

            return new MemberDTO
            {
                Id = memberEntity.Id,
                Name = memberEntity.Name,
                Description = memberEntity.Description,
                HeadshotUrl = memberEntity.HeadshotUrl,
                BirthDate = memberEntity.BirthDate,
                Role = memberEntity.Role,
                Movies = movieDTOs
            };
        }

        public static MemberDTO ToDTO(this MemberEntity memberEntity)
        {
            return new MemberDTO
            {
                Id = memberEntity.Id,
                Name = memberEntity.Name,
                Description = memberEntity.Description,
                HeadshotUrl = memberEntity.HeadshotUrl,
                BirthDate = memberEntity.BirthDate,
                Role = memberEntity.Role,
                Movies = new List<MovieDTO>()
            };
        }
    }
}