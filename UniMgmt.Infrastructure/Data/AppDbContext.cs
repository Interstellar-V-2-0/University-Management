using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;

namespace UniMgmt.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Section> Sections { get; set; }

}