using System;
using System.ComponentModel.DataAnnotations;

namespace IMBox.Services.Member.API.DTOs
{
    public record UpdateMemberDTO([Required] string Name, string Description, DateTime BirthDate, string Role);

}