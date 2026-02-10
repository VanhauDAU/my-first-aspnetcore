using Microsoft.AspNetCore.Mvc;
using MyFirstWebASP.Services;
using MyFirstWebASP.Models;
namespace MyFirstWebASP.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IActionResult> Index(string searchString, int? pageNumber)
    {
        ViewData["CurrentFilter"] = searchString;
        int pageSize = 5; 
        int pageIndex = pageNumber ?? 1;
        var students = await _studentService.GetStudentsAsync(searchString, pageIndex, pageSize);
    
        return View(students);
    }
    public IActionResult Create()
    {
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
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
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
