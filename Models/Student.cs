namespace MyFirstWebASP.Models;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public double Gpa { get; set; }
    public String Address { get; set;} = string.Empty;
}