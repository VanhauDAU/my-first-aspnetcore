using Microsoft.EntityFrameworkCore;
using MyFirstWebASP.Models;

namespace MyFirstWebASP.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
}