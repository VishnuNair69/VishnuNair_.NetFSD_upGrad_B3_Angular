using Microsoft.EntityFrameworkCore;

namespace Relationships.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: base(options)
        {
        }

        public DbSet<Employee> Employeess{ get; set; }   
        public DbSet<Dept> Depts{ get; set; }
        public DbSet<Student> Students{get;set;}
        public DbSet<Course> Courses{get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Dept)
                        .WithMany(d => d.Employees)
                        .HasForeignKey(e => e.Deptid);
            modelBuilder.Entity<Student>()
                        .HasOne(e=>e.Course)
                        .WithMany(d=>d.students)
                        .HasForeignKey(f=>f.Courseid);
        }

    }
}