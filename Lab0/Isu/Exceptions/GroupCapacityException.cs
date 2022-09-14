namespace Isu.Exceptions;

public class GroupCapacityException : Exception
{
    public GroupCapacityException(string message)
        : base(message)
    {
    }
}