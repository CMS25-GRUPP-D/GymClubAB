using Infrastructure.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class MemberRepository
{
    private readonly string _filePath = "members.json";
    private List<Member> _members;

    public MemberRepository()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _members = JsonSerializer.Deserialize<List<Member>>(json) ?? [];
        }
        else
        {
            
            _members = new List<Member>
            {
                new() { FirstName = "Alice", Membership = MembershipLevel.Bronze },
                new() { FirstName = "Bob", Membership = MembershipLevel.Silver }
            };
            SaveMembers(); 
        }
    }

    public Member GetMemberByName(string name)
    {
        return _members.FirstOrDefault(m => m.FirstName == name)!;
    }

    public void UpdateMember(Member member)
    {
        var index = _members.FindIndex(m => m.FirstName == member.FirstName);
        if (index >= 0)
        {
            _members[index] = member;
            SaveMembers();
        }
    }

    private void SaveMembers()
    {

        var json = JsonSerializer.Serialize(_members, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
