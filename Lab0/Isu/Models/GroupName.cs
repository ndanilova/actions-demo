using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private readonly string _value;

    public GroupName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[A-Za-z]{1}\d{4,6}$"))
            throw new GroupValidationException("Group name doesn't follow special template");
        _value = value;
    }

    public override string ToString()
    {
        return _value;
    }
}