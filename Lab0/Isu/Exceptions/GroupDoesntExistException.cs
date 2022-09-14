namespace Isu.Exceptions;

public class GroupDoesntExistException : Exception
{
    public GroupDoesntExistException(string message)
        : base(message)
    {
    }
}