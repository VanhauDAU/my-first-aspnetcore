using MyFirstWebASP.Models;

namespace MyFirstWebASP.Services;

public interface IStudentService 
{
    Task<List<Student>> GetTopStudentsAsync();
}
