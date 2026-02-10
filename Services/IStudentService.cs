using MyFirstWebASP.Models;

namespace MyFirstWebASP.Services;

public interface IStudentService 
{
    Task<Helpers.PaginatedList<Student>> GetStudentsAsync(string? searchString, int pageIndex, int pageSize);
}
