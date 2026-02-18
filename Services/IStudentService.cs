using MyFirstWebASP.Models;

namespace MyFirstWebASP.Services;

public interface IStudentService 
{
    Task<Helpers.PaginatedList<Student>> GetStudentsAsync(string? searchString, int pageIndex, int pageSize);
    Task<Student?> GetByIdAsync(int id);
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
    Task<List<Faculty>> GetFacultiesAsync();
    Task<List<Class>> GetClassesByFacultyAsync(int facultyId);
    Task<(Student? student, List<Faculty> faculties, int? selectedFacultyId)> GetStudentForEditAsync(int id);
}
