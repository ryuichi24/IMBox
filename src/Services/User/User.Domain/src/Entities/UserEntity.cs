using System;
using IMBox.Shared.Domain.Base;

namespace IMBox.Services.User.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Char Gender { get; set; }
        public string Continent { get; set; }
    }
}