using System;
using System.Collections.Generic;

namespace MyFirstWebASP.Models;

public partial class Class
{
    public int Id { get; set; }

    public string ClassName { get; set; } = null!;

    public string? Department { get; set; }

    public int? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
