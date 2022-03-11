using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Member.API.DTOs
{
    public record CreateMemberDTO([Required] string Name, string Description, DateTime BirthDate, string Role);

}