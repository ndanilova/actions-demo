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
        if (!_studentsByGroupName.ContainsKey(group))
            throw new GroupDoesntExistException("Can't add student. Group doesnt exist.");
        if (_studentsByGroupName[group].Count >= Group.MaxStudentsPerGroup)
            throw new GroupCapacityException("Group has reached maximum of students.");
        int currentStudentId = _nextId++;
        var student = new Student(currentStudentId, name);
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
        var group = _studentsByGroupName.Keys.SingleOrDefault(group => group.Name == groupName);
        var result = _studentsByGroupName[group];
        return result.ToImmutableArray();
    }

    public ImmutableArray<Student> FindStudents(CourseNumber courseNumber)
    {
        var result = new List<Student>();
        foreach (var group in _studentsByGroupName.Keys.Where(group => group.CourseNumber == courseNumber))
        {
            result.Concat(_studentsByGroupName[group]).ToList();
        }

        return result.ToImmutableArray();
    }

    public Group FindGroup(GroupName groupName)
    {
        return _studentsByGroupName.Keys.FirstOrDefault(group => group.Name == groupName);
    }

    public ImmutableArray<Group> FindGroups(CourseNumber courseNumber)
    {
        var result = _studentsByGroupName.Keys.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();
        return result.ToImmutableArray();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        GetStudent(student.Id);
        if (FindGroup(newGroup.Name) == null)
            throw new GroupDoesntExistException("Can't change student's group. Group not found");
        if (_studentsByGroupName[newGroup].Count >= Group.MaxStudentsPerGroup)
            throw new GroupCapacityException("new group has reached maximum of students.");

        foreach (Group group in _studentsByGroupName.Keys)
        {
            if (!_studentsByGroupName[group].Contains(student)) continue;
            _studentsByGroupName[newGroup].Add(student);
            _studentsByGroupName[group].Remove(student);
        }
    }
}