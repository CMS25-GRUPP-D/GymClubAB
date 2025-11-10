using Infrastructure.DTOs;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;
public interface IMemberMapper
{
    // member -> memberpdaterequest (edit-metod listviewmodel)
    MemberUpdateRequest MapFromMemberToUpdateRequest(Member member);
    // memberupdaterequest -> member (UpdateMemberAsync memberservice)
    void MapFromUpdateRequestToMember(Member existing, MemberUpdateRequest updateRequest);
}
