namespace Isu.Exceptions;

public class GroupNameValidationException : Exception
{
    public GroupNameValidationException(string message)
        : base(message)
    {
    }
}