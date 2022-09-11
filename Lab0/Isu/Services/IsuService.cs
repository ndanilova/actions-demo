using Isu.Entities;
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
        int currentStudentId = _nextId++;
        var student = new Student(currentStudentId, name);
        _studentsByGroupName[group].Add(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        return _studentsByGroupName.Values.SelectMany(x => x).SingleOrDefault(student => student.Id == id)
            ?? throw new Exception("Student with id not found");
    }

    public Student FindStudent(int id)
    {
        try
        {
            return GetStudent(id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _studentsByGroupName.Keys.Where(group => group.Name == groupName).Select(group => _studentsByGroupName[group]).FirstOrDefault();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return (from @group in _studentsByGroupName.Keys let charNumber = @group.Name.ToString()[2] let number = charNumber - '0' where new CourseNumber(number).Equals(courseNumber) from student in _studentsByGroupName[@group] select student).ToList();
    }

    public Group FindGroup(GroupName groupName)
    {
        return _studentsByGroupName.Keys.FirstOrDefault(group => group.Name == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _studentsByGroupName.Keys.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        try
        {
            GetStudent(student.Id);
        }
        catch (Exception)
        {
            throw new Exception("Can't change student's group. Student not found");
        }

        if (FindGroup(newGroup.Name) == null)
            throw new Exception("Can't change student's group. Group not found");

        foreach (Group group in _studentsByGroupName.Keys.Where(group => _studentsByGroupName[group].Contains(student)))
        {
            _studentsByGroupName[group].Remove(student);
            _studentsByGroupName[newGroup].Add(student);
        }
    }
}