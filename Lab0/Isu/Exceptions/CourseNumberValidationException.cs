namespace Isu.Exceptions;

public class CourseNumberValidationException : Exception
{
    public CourseNumberValidationException(string message)
    : base(message)
    {
    }
}