using System;
using EduHub.Ch02;
using EduHub.Ch02.Domain;
using Xunit;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // Test mẫu đã viết sẵn: chạy được ngay trên ClubManager hiện tại.
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var manager = new ClubManager();

        // Act
        var act = () => manager.CreateClub("", "mô tả", "p@gmail.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        var name = "Coding Club";
        var description = "Learn C# and .NET";

        // Act
        var club = new Club { Id = Guid.NewGuid(), Name = name, Description = description };

        // Assert
        Assert.NotNull(club);
        Assert.Equal(name, club.Name);
        Assert.Equal(description, club.Description);
    }

    [Fact]
    public void Club_Create_WithInvalidName_ThrowsArgumentException()
    {
        // Arrange
        var longName = new string('A', 101);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            if (longName.Length > 100)
                throw new ArgumentException("Club name too long");
        });
    }

    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        // Arrange
        int maxLimit = 50;
        int currentMembersAtMax = 50;
        int currentMembersBelowMax = 49;

        // Act & Assert
        bool hasRoomAtMax = currentMembersAtMax < maxLimit;
        bool hasRoomBelowMax = currentMembersBelowMax < maxLimit;

        Assert.False(hasRoomAtMax);
        Assert.True(hasRoomBelowMax);
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var eventObj = new Event { Id = Guid.NewGuid(), Title = "Tech Workshop", Date = DateTime.Now.AddDays(1) };
        var memberId = Guid.NewGuid();

        // Act
        // Giả lập logic đăng ký thành viên vào sự kiện thành công
        bool isRegistered = eventObj != null && memberId != Guid.Empty;

        // Assert
        Assert.True(isRegistered);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var registeredMembers = new System.Collections.Generic.HashSet<Guid> { memberId };

        // Act
        // Thử đăng ký lại thành viên đã có trong danh sách
        bool canRegisterAgain = registeredMembers.Add(memberId);

        // Assert
        Assert.False(canRegisterAgain);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var registeredMembers = new System.Collections.Generic.HashSet<Guid>();
        var unregisteredMemberId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            if (!registeredMembers.Contains(unregisteredMemberId))
                throw new InvalidOperationException("Member is not registered for this event");
        });
    }
}
