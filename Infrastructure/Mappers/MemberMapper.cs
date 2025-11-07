using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Mappers;

public class MemberMapper : IMemberMapper
{
    public MemberUpdateRequest MapFromMemberToUpdateRequest(Member member)
    {
        MemberUpdateRequest updateRequest = new()
        {
            SocialSecurityNumber = member.SocialSecurityNumber,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phonenumber = member.Phonenumber,
            Membership = member.Membership
        };

        return updateRequest;
    }

    public Member MapFromUpdateRequestToMember(MemberUpdateRequest updateRequest)
    {
        Member member = new()
        {
            SocialSecurityNumber = updateRequest.SocialSecurityNumber,
            FirstName = updateRequest.FirstName,
            LastName = updateRequest.LastName,
            Email = updateRequest.Email,
            Phonenumber = updateRequest.Phonenumber,
            Membership = updateRequest.Membership
        };

        return member;
    }
}
