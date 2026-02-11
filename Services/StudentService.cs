using Microsoft.EntityFrameworkCore;
using MyFirstWebASP.Models;

namespace MyFirstWebASP.Services;

public class StudentService : IStudentService
{
    private readonly SchoolDBContext _context;

    public StudentService(SchoolDBContext context)
    {
        _context = context;
    }

    public async Task<Helpers.PaginatedList<Student>> GetStudentsAsync(string? searchString, int pageIndex, int pageSize)
    {
        var query = _context.Students
        .Include(s => s.Class)
        .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(s => s.FullName.Contains(searchString) 
                                || (s.Address != null && s.Address.Contains(searchString))
                                || (s.Class != null && s.Class.ClassName.Contains(searchString)));
        }

        query = query.OrderByDescending(s => s.Gpa);

        return await Helpers.PaginatedList<Student>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task AddStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}