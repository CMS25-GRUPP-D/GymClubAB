using Infrastructure.DTOs;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<ResponseResult> SaveMemberAsync(Member member);
    Task<bool> UpdateMemberAsync(MemberUpdateRequest memberRequest);

    Task<ResponseResult<bool>> DeleteMemberAsync(string ssn);

    Task<Member> GetMemberByIdAsync(string id);
    Task<ResponseResult<IEnumerable<Member>>> GetAllMembersAsync();
}
