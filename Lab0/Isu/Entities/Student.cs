using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Entities;

public class Student
{
    public Student(int id, string name)
    {
        if (!Regex.IsMatch(id.ToString(), @"^[1-9]\d{5}$"))
        {
            throw new StudentValidationException("Id is not valid");
        }

        if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[A-Z][A-Za-z]+"))
        {
            throw new Exception("name is not valid");
        }

        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; }
}