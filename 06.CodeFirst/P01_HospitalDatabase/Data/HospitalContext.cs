using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visitation> Visitations { get; set; }
        public virtual DbSet<Diagnose> Diagnoses { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }


        public HospitalContext()
        {

        }

        protected HospitalContext(DbContextOptions options):base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Connection, b => b.MigrationsAssembly("Maintenance"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50)
                    ;
                entity.Property(e => e.LastName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50)
                    ;
                entity.Property(e => e.Address)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250)
                    ;
                entity.Property(e => e.Email)
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasMaxLength(80)
                    ;
                entity.Property(e => e.HasInsurance)
                    .HasDefaultValue(true)
                    ;
            });
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
            });
            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .HasMaxLength(250)
                    .IsUnicode();

                entity.HasOne(p => p.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Visitation_Patient");

                entity.HasOne(e => e.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(e => e.DoctorId);
            });
            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .HasMaxLength(250)
                    .IsUnicode();
                entity.HasOne(p => p.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId);
            });
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);
               
            });
            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new {e.PatientId, e.MedicamentId});
                entity.HasOne(e => e.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Prescriptions)
                    .HasForeignKey(e => e.PatientId);
            });
            
        }
    }
}