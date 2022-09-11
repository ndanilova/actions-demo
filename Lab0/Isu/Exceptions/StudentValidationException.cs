namespace Isu.Exceptions;

public class StudentValidationException : Exception
{
    public StudentValidationException(string message)
        : base(message)
    {
    }
}