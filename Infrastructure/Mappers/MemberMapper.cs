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

    public void MapFromUpdateRequestToMember(Member existing, MemberUpdateRequest updateRequest)
    {
        existing.FirstName = updateRequest.FirstName;
        existing.LastName = updateRequest.LastName;
        existing.Email = updateRequest.Email;
        existing.Phonenumber = updateRequest.Phonenumber;
        existing.Membership = updateRequest.Membership;
        existing.PostalCode = updateRequest.PostalCode;
    }
}
