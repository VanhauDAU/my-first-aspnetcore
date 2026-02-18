using System;
using System.Collections.Generic;

namespace MyFirstWebASP.Models;

public partial class Faculty
{
    public int Id { get; set; }

    public string FacultyName { get; set; } = null!;

    public string FacultyCode { get; set; } = null!;

    public string? Description { get; set; }

    public int? EstablishedYear { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
