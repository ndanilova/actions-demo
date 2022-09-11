using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    public CourseNumber(int number)
    {
        if (number is < 1 or > 4)
            throw new CourseNumberValidationException("Course number should be between 1 and 4");
        Number = number;
    }

    private int Number { get; init; }

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