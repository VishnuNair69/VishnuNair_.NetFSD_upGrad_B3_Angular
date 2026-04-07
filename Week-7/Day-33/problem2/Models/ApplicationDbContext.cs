using Microsoft.EntityFrameworkCore;
namespace ContactManagementSystem.Models{
public class ApplicationDbContext : DbContext
{
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
 : base(options)
 {
 }
 public DbSet<ContactInfo> ContactInfos { get; set; }
public DbSet<Company> Companies { get; set; }
public DbSet<Department> Departments { get; set; }
 protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactInfo>()
                        .HasOne(e => e.Company)
                        .WithMany(d => d.ContactInfos)
                        .HasForeignKey(e => e.Companyid);
            modelBuilder.Entity<ContactInfo>()
                        .HasOne(e => e.Department)
                        .WithMany(d => d.ContactInfos)
                        .HasForeignKey(e => e.Departmentid);
        }
}
}