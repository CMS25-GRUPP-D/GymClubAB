using Infrastructure.DTOs;

namespace Infrastructure.Interfaces;
public interface IMemberMapper
{
    // member -> memberpdaterequest (edit-metod listviewmodel)
    MemberUpdateRequest MapFromMemberToUpdateRequest(MemberUpdateRequest member);
    // memberupdaterequest -> member (UpdateMemberAsync memberservice)
    MemberUpdateRequest MapFromUpdateRequestToMember(MemberUpdateRequest updateRequest);
}
