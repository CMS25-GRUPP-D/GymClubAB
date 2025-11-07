using Infrastructure.DTOs;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;
public interface IMemberMapper
{
    // member -> memberpdaterequest (edit-metod listviewmodel)
    MemberUpdateRequest MapFromMemberToUpdateRequest(Member member);
    // memberupdaterequest -> member (UpdateMemberAsync memberservice)
    Member MapFromUpdateRequestToMember(MemberUpdateRequest updateRequest);
}
