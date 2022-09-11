using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public Group(GroupName value)
    {
        Name = value;
        CourseNumber = new CourseNumber(Name.ToString()[2] - '0');
    }

    public GroupName Name { get; private set; }
    public CourseNumber CourseNumber { get; init; }
}