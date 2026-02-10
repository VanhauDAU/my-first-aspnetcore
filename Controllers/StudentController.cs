using Microsoft.AspNetCore.Mvc;
using MyFirstWebASP.Services;

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
}
