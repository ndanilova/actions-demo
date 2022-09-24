using Isu.Models;
using static System.Int32;

namespace Isu.Entities;

public class Group
{
    public const int MaxStudentsPerGroup = 40;
    public Group(GroupName value)
    {
        Name = value;
        TryParse(value.ToString()[2].ToString(), out int parseResult);
        CourseNumber = new CourseNumber(parseResult);
        }

    public GroupName Name { get; }
    public CourseNumber CourseNumber { get; }
}