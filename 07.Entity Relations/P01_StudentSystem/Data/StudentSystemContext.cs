



namespace P01_StudentSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //STUDENT
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100)
                    ;
                entity.Property(e => e.PhoneNumber)
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasColumnType("CHAR(10)");

                entity.Property(e => e.RegisteredOn)
                    .HasColumnType("DATETIME2");
              
                entity.Property(e => e.Birthday)
                    .IsRequired(false);
                 
                
            });
         
                        //COURSE
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
                entity.HasKey(c => c.CourseId);

                entity.Property(p => p.Name)
                    .IsUnicode(true)
                    .HasMaxLength(80)
                    ;
                entity.Property(e => e.Description)
                    .IsRequired(false)
                    .IsUnicode(true);

                entity.Property(p => p.StartDate)
                    .HasColumnType("DATETIME2");

                entity.Property(p => p.EndDate)
                    .HasColumnType("DATETIME2");
            });
           

                //RESOURSE
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resourses");

                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsUnicode(false); 
                
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasOne(r => r.Course)
                    .WithMany(r => r.Resources)
                    .HasForeignKey(r=>r.CourseId);
            });

            //HOMEWORK
            modelBuilder.Entity<Homework>(entity =>
            {
                entity.ToTable("HomeworkSubmissions");

                entity.HasKey(h => h.HomeworkId);

                entity.Property(c => c.Content)
                    .IsUnicode(false);

                entity.HasOne(h => h.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(h => h.CourseId);

                entity.HasOne(h => h.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(h => h.StudentId);
            });

            //STUDENTCOURSE
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourses");

                entity.HasKey(e => new { e.StudentId, e.CourseId });

                entity.HasOne(sc => sc.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(sc => sc.StudentId);

                entity.HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentsEnrolled)
                    .HasForeignKey(sc => sc.CourseId);
            });


        }
    }
}