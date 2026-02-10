using Microsoft.AspNetCore.Mvc;
using MyFirstWebASP.Services;

namespace MyFirstWebASP.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    // Inject Service vào thông qua Constructor
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IActionResult> Index()
    {
        // Controller gọi Service xử lý, nó không quan tâm DB lấy dữ liệu thế nào
        var students = await _studentService.GetTopStudentsAsync();
        
        // Trả về View và đính kèm dữ liệu (Model)
        return View(students);
    }
}
