using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private const int MaxNumberOfCourse = 4;
    private const int MinNumberOfCourse = 1;
    public CourseNumber(int number)
    {
        if (number is < MinNumberOfCourse or > MaxNumberOfCourse)
            throw new CourseNumberValidationException("Course number should be between 1 and 4");
        Number = number;
    }

    private int Number { get; }

    public bool Equals(CourseNumber other)
    {
        return Number == other.Number;
    }

    public override int GetHashCode()
    {
        return Number;
    }

    public override string ToString()
    {
        return Number.ToString();
    }
}