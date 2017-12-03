



namespace Employees.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Employees.Models;

    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeesContext()
        {
            
        }
        protected EmployeesContext(DbContextOptions options):base (options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfiguration.ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Address)
                    .IsRequired(false)
                    .HasMaxLength(250);

                entity.HasOne(e => e.Manager)
                    .WithMany(m => m.Employees)
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
