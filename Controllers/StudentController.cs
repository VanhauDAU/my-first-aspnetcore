using Microsoft.AspNetCore.Mvc;
using MyFirstWebASP.Services;
using MyFirstWebASP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        int pageSize = 8; 
        int pageIndex = pageNumber ?? 1;
        var students = await _studentService.GetStudentsAsync(searchString, pageIndex, pageSize);
    
        return View(students);
    }
    public async Task<IActionResult> Create()
    {
        var faculties = await _studentService.GetFacultiesAsync();
        ViewBag.FacultyList = new SelectList(faculties, "Id", "FacultyName");
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> GetClassesByFaculty(int facultyId)
    {
        var classes = await _studentService.GetClassesByFacultyAsync(facultyId);
        return Json(classes.Select(c => new { id = c.Id, className = c.ClassName }));
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
        var (student, faculties, selectedFacultyId) = await _studentService.GetStudentForEditAsync(id);
        if (student == null) return NotFound();
        
        ViewBag.FacultyList = new SelectList(faculties, "Id", "FacultyName");
        ViewBag.SelectedFacultyId = selectedFacultyId;
        
        // Load lớp của khoa được chọn
        if (selectedFacultyId.HasValue)
        {
            var classes = await _studentService.GetClassesByFacultyAsync(selectedFacultyId.Value);
            ViewBag.ClassList = new SelectList(classes, "Id", "ClassName", student.ClassId);
        }
        else
        {
            ViewBag.ClassList = new SelectList(new List<Class>(), "Id", "ClassName");
        }
        
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
