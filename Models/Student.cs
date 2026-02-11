using System;
using System.Collections.Generic;

namespace MyFirstWebASP.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Address { get; set; }

    public double? Gpa { get; set; }

    public int? ClassId { get; set; }

    public string? Phone { get; set; }

    public virtual Class? Class { get; set; }
}
