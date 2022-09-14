using System.Text.RegularExpressions;
using Isu.Entities;
using Isu.Models;
using Group = Isu.Entities.Group;

namespace Isu.Services;

public static class Programm
{
    public static void Main()
    {
        var isu = new IsuService();
        var nm = new GroupName("M32111");
        var nm2 = new GroupName("M32101");
        Group group = isu.AddGroup(new GroupName("8484848"));
        var group2 = isu.AddGroup(nm2);
        isu.AddStudent(group, "jenny");
        isu.AddStudent(group, "josh");
        isu.AddStudent(group, "tom");
        isu.AddStudent(group2, "tim");
        isu.AddStudent(group2, "sam");
        isu.AddStudent(group2, "piter");
        var student = isu.FindStudent(100001);
        isu.ChangeStudentGroup(student, new Group(new GroupName("M32456")));
        var list = isu.FindStudents(new GroupName(null));
        foreach (Student st in list)
        {
            Console.WriteLine(st.Name);
        }

        list = isu.FindStudents(new CourseNumber(1));
        foreach (Student st in list)
        {
            Console.WriteLine(st.Id);
        }

        Regex regex = new Regex(@"\[A-Za-z]{1}\d{4,5}");
        bool boo = Regex.IsMatch("M32111", @"^[A-Za-z]{1}\d{4,5}$");
        Console.WriteLine(boo);
    }
}