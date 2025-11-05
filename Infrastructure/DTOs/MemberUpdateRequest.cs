using Infrastructure.Models;

namespace Infrastructure.DTOs;

public class MemberUpdateRequest // vilka prop ska ingå?
{
    public string SocialSecurityNumber { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phonenumber { get; set; }
    public MembershipLevel Membership { get; set; } = MembershipLevel.None;
}