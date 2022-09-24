using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    private readonly IsuService _isu = new IsuService();
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        // Arrange
        var group = _isu.AddGroup(new GroupName("M32111"));

        // Act
        var student = _isu.AddStudent(group, "Vasily");

        // Assert
        Assert.Equal(_isu.FindStudent(student.Id), student);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        // Arrange
        var group = _isu.AddGroup(new GroupName("F34999"));
        for (int i = 0; i < Group.MaxStudentsPerGroup; i++)
        {
            _isu.AddStudent(group, "Morgenshtern");
        }

        // Act and Assert
        Assert.Throws<GroupCapacityException>(() => _isu.AddStudent(group, "Slava"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        // Act and assert
        Assert.Throws<GroupNameValidationException>(() => _isu.AddGroup(new GroupName("K70")));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        // Arrange
        var groupPrevious = _isu.AddGroup(new GroupName("P32949"));
        var groupNew = _isu.AddGroup(new GroupName("K31450"));
        var student = _isu.AddStudent(groupPrevious, "Vasily");

        // Act
        _isu.ChangeStudentGroup(student, groupNew);

        // Assert
        Assert.Contains(_isu.FindStudents(groupNew.Name), student1 => student1 == student);
    }
}