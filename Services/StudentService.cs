using Microsoft.EntityFrameworkCore;
using MyFirstWebASP.Data;
using MyFirstWebASP.Models;

namespace MyFirstWebASP.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;

    public StudentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Helpers.PaginatedList<Student>> GetStudentsAsync(string? searchString, int pageIndex, int pageSize)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(s => s.FullName.Contains(searchString) 
                                || s.Address.Contains(searchString));
        }

        query = query.OrderByDescending(s => s.Gpa);

        return await Helpers.PaginatedList<Student>.CreateAsync(query, pageIndex, pageSize);
    }
}
