namespace Isu.Exceptions;

public class StudentDoesntExistException : Exception
{
    public StudentDoesntExistException(string message)
        : base(message)
    {
    }
}