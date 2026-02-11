using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyFirstWebASP.Models;

public partial class SchoolDBContext : DbContext
{
    public SchoolDBContext()
    {
    }

    public SchoolDBContext(DbContextOptions<SchoolDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SchoolDB;User Id=sa;Password=Vanhau1410@;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC071F913AA0");

            entity.Property(e => e.ClassName).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(100);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07B0C17CCD");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Student_Class");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
