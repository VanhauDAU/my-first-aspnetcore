using Microsoft.AspNetCore.Mvc;
using MyFirstWebASP.Services;
using MyFirstWebASP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace MyFirstWebASP.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly SchoolDBContext _context;

    public StudentController(IStudentService studentService, SchoolDBContext context)
    {
        _studentService = studentService;
        _context = context;
    }

    public async Task<IActionResult> Index(string searchString, int? pageNumber)
    {
        ViewData["CurrentFilter"] = searchString;
        int pageSize = 5; 
        int pageIndex = pageNumber ?? 1;
        var students = await _studentService.GetStudentsAsync(searchString, pageIndex, pageSize);
    
        return View(students);
    }
    public async Task<IActionResult> Create()
    {
        var classes = await _context.Classes.OrderBy(c => c.ClassName).ToListAsync();
        ViewBag.ClassList = new SelectList(classes, "Id", "ClassName");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> Create(MyFirstWebASP.Models.Student student)
    {
        if (ModelState.IsValid)
        {
            await _studentService.AddStudentAsync(student);
            return RedirectToAction(nameof(Index)); 
        }
        return View(student); 
    }
    public async Task<IActionResult> Edit(int id)
    {
        if(id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        var classes = await _context.Classes.OrderBy(c => c.ClassName).ToListAsync();
        ViewBag.ClassList = new SelectList(classes, "Id", "ClassName", student.ClassId);
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            await _studentService.UpdateStudentAsync(student);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteStudentAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
