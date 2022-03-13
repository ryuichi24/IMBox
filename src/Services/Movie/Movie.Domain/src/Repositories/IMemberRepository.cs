using IMBox.Services.Movie.Domain.Entities;
using IMBox.Shared.Domain.Repository;

namespace IMBox.Services.Movie.Domain.Repositories
{
    public interface IMemberRepository : ICanCreate<MemberEntity>, ICanUpdate<MemberEntity>, ICanRemove, ICanGetById<MemberEntity>, ICanGetAll<MemberEntity>
    {
    }
}