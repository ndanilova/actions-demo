using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Entities;

public class Student
{
    private const string IdPattern = @"^[1-9]\d{5}$";
    private const string NamePattern = @"^[A-Z][A-Za-z]+";

    public Student(int id, string name)
    {
        Match matchId = Regex.Match(id.ToString(), IdPattern);
        Match matchName = Regex.Match(name, NamePattern);
        if (!matchId.Success)
        {
            throw new StudentValidationException("Id is not valid");
        }

        if (!matchName.Success)
        {
            throw new Exception("name is not valid");
        }

        Id = id;
        Name = name;
    }

    public int Id { get;  }
    public string Name { get;  }
}