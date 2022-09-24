using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private readonly string _value;

    public GroupName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !ValidGroupName.IsMatch(value))
            throw new GroupNameValidationException("Group name doesn't follow special template");
        _value = value;
    }

    private static Regex ValidGroupName => new (@"^[A-Za-z]{1}\d{4,6}$", RegexOptions.Compiled);
    public override string ToString()
    {
        return _value;
    }
}