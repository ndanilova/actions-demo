using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Entities;

public class Student
{
    private static readonly Regex ValidId = new (@"^[1-9]\d{5}$", RegexOptions.Compiled);
    private static readonly Regex ValidName = new Regex(@"^[A-Z][A-Za-z]+", RegexOptions.Compiled);
    public Student(int id, string name)
    {
        if (!ValidId.IsMatch(id.ToString()))
        {
            throw new StudentValidationException("Id is not valid");
        }

        if (!ValidName.IsMatch(name))
        {
            throw new Exception("name is not valid");
        }

        Id = id;
        Name = name;
    }

    public int Id { get;  }
    public string Name { get;  }
}