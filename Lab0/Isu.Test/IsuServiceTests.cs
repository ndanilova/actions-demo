using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;
using Xunit.Sdk;

namespace Isu.Test;

public class IsuServiceTests
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        // Arrange
        var isu = new IsuService();
        var group = isu.AddGroup(new GroupName("M32111"));

        // Act
        var student = isu.AddStudent(group, "Vasily");

        // Assert
        Assert.Equal(isu.FindStudent(student.Id), student);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        // Arrange
        var isu = new IsuService();

        // Act and assert
        Assert.Throws<GroupValidationException>(() => isu.AddGroup(new GroupName("K70")));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        // Arrange
        var isu = new IsuService();
        var groupPrevious = isu.AddGroup(new GroupName("P32949"));
        var groupNew = isu.AddGroup(new GroupName("K31450"));
        var student = isu.AddStudent(groupPrevious, "Vasily");

        // Act
        isu.ChangeStudentGroup(student, groupNew);

        // Assert
        Assert.Contains(isu.FindStudents(groupNew.Name), student1 => student1 == student);
    }
}