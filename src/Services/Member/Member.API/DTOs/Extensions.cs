using IMBox.Services.Member.Domain.Entities;

namespace IMBox.Services.Member.API.DTOs
{
    public static class Extensions
    {
        public static MemberDTO ToDTO(this MemberEntity memberEntity)
        {
            return new MemberDTO
            {
                Id = memberEntity.Id,
                Name = memberEntity.Name,
                Description = memberEntity.Description,
                BirthDate = memberEntity.BirthDate,
                Role = memberEntity.Role
            };
        }
    }
}