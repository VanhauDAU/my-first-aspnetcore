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

    public async Task<List<Student>> GetTopStudentsAsync()
    {
        return await _context.Students
                             .Where(s => s.Gpa >= 3.2)
                             .OrderByDescending(s => s.Gpa)
                             .ToListAsync();
    }
}
