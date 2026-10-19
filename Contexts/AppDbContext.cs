using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Models;

namespace UniversidadAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Docente> Docentes => Set<Docente>();
    public DbSet<Asignacion> Asignaciones => Set<Asignacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Asignacion>()
            .HasOne<Curso>()
            .WithMany()
            .HasForeignKey(a => a.CursoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Asignacion>()
            .HasOne<Estudiante>()
            .WithMany()
            .HasForeignKey(a => a.EstudianteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Asignacion>()
            .HasOne<Docente>()
            .WithMany()
            .HasForeignKey(a => a.DocenteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
