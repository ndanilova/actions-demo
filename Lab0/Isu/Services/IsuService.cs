using System.Collections.Immutable;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly Dictionary<Group, List<Student>> _studentsByGroupName = new Dictionary<Group, List<Student>>();
    private int _nextId = 100000;

    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        _studentsByGroupName.Add(group, new List<Student>());
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (!TryFindGroup(group))
            throw new GroupDoesntExistException("Can't add student. Group doesnt exist.");
        if (!CheckGroupCapacity(group))
            throw new GroupCapacityException("Group has reached maximum of students.");
        var student = new Student(_nextId++, name);
        _studentsByGroupName[group].Add(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        return _studentsByGroupName.Values.SelectMany(x => x).SingleOrDefault(student => student.Id == id)
               ?? throw new StudentDoesntExistException("Student with id not found");
    }

    public Student FindStudent(int id)
    {
        try
        {
            return GetStudent(id);
        }
        catch (StudentDoesntExistException)
        {
            return null;
        }
    }

    public ImmutableArray<Student> FindStudents(GroupName groupName)
    {
        if (groupName == null)
            throw new GroupNameValidationException("null argument given for group name");
        var group = _studentsByGroupName.Keys.SingleOrDefault(group => group.Name == groupName);
        var result = _studentsByGroupName[group];
        return result.ToImmutableArray();
    }

    public ImmutableArray<Student> FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber == null)
            throw new CourseNumberValidationException("null argument given for course number");
        var result = _studentsByGroupName.Keys.Where(group => group.CourseNumber == courseNumber).SelectMany(x => _studentsByGroupName[x]);
        return result.ToImmutableArray();
    }

    public Group FindGroup(GroupName groupName)
    {
        if (groupName == null)
            throw new GroupNameValidationException("null argument given for group name");
        return _studentsByGroupName.Keys.FirstOrDefault(group => group.Name == groupName);
    }

    public ImmutableArray<Group> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber == null)
            throw new CourseNumberValidationException("null argument given for course number");
        var result = _studentsByGroupName.Keys.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();
        return result.ToImmutableArray();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        GetStudent(student.Id);
        if (!TryFindGroup(newGroup))
            throw new GroupDoesntExistException("Can't change student's group. Group not found");
        if (!CheckGroupCapacity(newGroup))
            throw new GroupCapacityException("new group has reached maximum of students.");

        foreach (Group group in _studentsByGroupName.Keys)
        {
            if (!_studentsByGroupName[group].Contains(student)) continue;
            _studentsByGroupName[newGroup].Add(student);
            _studentsByGroupName[group].Remove(student);
        }
    }

    private bool TryFindGroup(Group group)
    {
        return _studentsByGroupName.ContainsKey(group);
    }

    private bool CheckGroupCapacity(Group group)
    {
        return _studentsByGroupName[group].Count < Group.MaxStudentsPerGroup;
    }
}