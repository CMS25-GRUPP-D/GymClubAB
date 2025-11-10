using Infrastructure.DTOs;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<ResponseResult> SaveMemberAsync(Member member);
    Task<ResponseResult> UpdateMemberAsync(MemberUpdateRequest updateRequest);
    Task<ResponseResult<bool>> DeleteMemberAsync(string id);
    Task<Member> GetMemberByIdAsync(string id);
    Task<ResponseResult<IEnumerable<Member>>> GetAllMembersAsync();
}
