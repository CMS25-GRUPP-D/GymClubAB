using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<ResponseResult> SaveMemberAsync(Member member);
    Task<bool> UpdateMemberAsync(Member member);
    Task<bool> DeleteMemberAsync(string id);
    Task<Member> GetMemberByIdAsync(string id);
    Task<ResponseResult<IEnumerable<Member>>> GetAllMembersAsync();
}
