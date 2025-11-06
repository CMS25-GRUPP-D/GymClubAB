using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Services;

public class MemberService_Tests
{
    [Fact]
    public async Task SaveMemberAsync_ShouldSaveMember_When_Data_Is_Valid()
    {
        // Arrange
        var mockRepo = new Mock<IJsonRepository>();
        var service = new MemberService(mockRepo.Object);

        var member = new Member
        {
            SocialSecurityNumber = "19900101-1234",
        };

        // Act
        var result = await service.SaveMemberAsync(member);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Medlemmen har sparats.", result.Message);
    }

    [Fact]
    public async Task SaveMemberAsync_ShouldReturnError_When_Member_Is_Null()
    {
        // Arrange
        var mockRepo = new Mock<IJsonRepository>();
        var service = new MemberService(mockRepo.Object);

        // Act
        var result = await service.SaveMemberAsync(null);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Otillräcklig data för medlem.", result.Message);
    }

    [Fact]
    public async Task SaveMemberAsync_ShouldReturnError_WhenSocialSecurityNumberIsInvalid()
    {
        // Arrange
        var mockRepo = new Mock<IJsonRepository>();
        var service = new MemberService(mockRepo.Object);

        var member = new Member
        {
            SocialSecurityNumber = "ABC123"
        };

        // Act
        var result = await service.SaveMemberAsync(member);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Ogiltigt personnummer format.", result.Message);
    }

    [Fact]
    public async Task DeleteMemberAsync_ShouldReturnSuccess_When_Member_Exists()
    {
        // Arrange
        var mockRepo = new Mock<IJsonRepository>();
        var service = new MemberService(mockRepo.Object);

        // First add a member to delete
        var member = new Member
        {
            SocialSecurityNumber = "19900101-1234",
            PostalCode = "12345"
        };
        await service.SaveMemberAsync(member);

        // Act
        var result = await service.DeleteMemberAsync("19900101-1234");

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Member deleted", result.Message);
        mockRepo.Verify(x => x.SaveContentToFileAsync(It.IsAny<IEnumerable<Member>>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task DeleteMemberAsync_ShouldReturnError_When_Member_Does_Not_Exist()
    {
        // Arrange
        var mockRepo = new Mock<IJsonRepository>();
        var service = new MemberService(mockRepo.Object);

        // Act
        var result = await service.DeleteMemberAsync("19900101-9999");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Could not delete user", result.Message);
    }
}
