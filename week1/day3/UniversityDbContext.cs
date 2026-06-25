using ConsoleApp1.ClassMapping;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

public class UniversityDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Syllabus> Syllabi { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new GradeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new SyllabusConfiguration());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=University2;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}