using System;

namespace IMBox.Services.Member.API.DTOs
{
    public record MemberDTO(Guid Id, string Name, string Description, DateTime BirthDate, string Role);

}