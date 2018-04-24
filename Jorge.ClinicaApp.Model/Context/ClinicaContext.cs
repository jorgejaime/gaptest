using System;
using Jorge.ClinicaApp.Model.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Jorge.ClinicaApp.Model.Context
{
    public partial class ClinicaContext : DbContext
    {

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<AppointmentType> AppointmentType { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        public ClinicaContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Patient>(entity =>
            {

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ege)
                   .IsRequired();

                entity.Property(e => e.Gender)
                   .IsRequired();

            });

            modelBuilder.Entity<Appointment>(entity =>
            {

                entity.Property(e => e.AppointmentDate)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .HasConstraintName("FK_Appointment_AppointmentType");

                entity.HasOne(d => d.Patient)
                .WithMany(p => p.Appointment)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Appointment_Patient");
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(25);
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(15);
                });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_User");
            });
        }
    }
}
