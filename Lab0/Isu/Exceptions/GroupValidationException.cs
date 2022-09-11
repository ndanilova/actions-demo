namespace Isu.Exceptions;

public class GroupValidationException : Exception
{
    public GroupValidationException(string message)
        : base(message)
    {
    }
}