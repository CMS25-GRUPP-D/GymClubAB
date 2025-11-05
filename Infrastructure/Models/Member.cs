namespace Infrastructure.Models;
public enum MembershipLevel
{
    None,
    Bronze,
    Silver,
    Gold
}

public class Member
{
    public string SocialSecurityNumber { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phonenumber { get; set; }
    public MembershipLevel Membership { get; set; } = MembershipLevel.None;
}
