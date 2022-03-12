using IMBox.Services.Member.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Member.Domain.Repositories
{
    public interface IMemberRepository : ICanCreate<MemberEntity>, ICanUpdate<MemberEntity>, ICanRemove, ICanGetById<MemberEntity>, ICanGetAll<MemberEntity>
    {
    }
}