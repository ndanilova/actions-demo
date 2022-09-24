using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int MaxStudentsPerGroup = 40;
    public Group(GroupName value)
    {
        Name = value;
        CourseNumber = new CourseNumber(Name.ToString()[2] - '0');
    }

    public GroupName Name { get; }
    public CourseNumber CourseNumber { get; }
}