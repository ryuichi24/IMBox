using System;
using System.Collections.Generic;
using System.Linq;
using IMBox.Services.User.API.DTOs;
using IMBox.Services.User.Domain.Entities;

namespace IMBox.Services.Member.API.DTOs
{
    public static class Extensions
    {
        public static UserDTO ToDTO(this UserEntity userEntity)
        {
            return new UserDTO
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
                Gender = userEntity.Gender,
                BirthDate = userEntity.BirthDate,
                Country = userEntity.Country,
                Roles = userEntity.Roles,
                CreatedAt = userEntity.CreatedAt
            };
        }
    }
}