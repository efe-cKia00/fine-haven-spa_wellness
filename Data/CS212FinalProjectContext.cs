using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS212FinalProject.Models;

namespace CS212FinalProject.Data
{
    public class CS212FinalProjectContext : DbContext
    {
        public CS212FinalProjectContext(DbContextOptions<CS212FinalProjectContext> options)
            : base(options)
        {
        }

        // Use plural DbSet names
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<Appointment> Appointments { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configuration: store enum as string, default to Customer, and ensure unique emails
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Role)
                      .HasConversion<string>()
                      .HasDefaultValue(RoleType.Customer);

                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.FirstName).HasMaxLength(50);
                entity.Property(u => u.LastName).HasMaxLength(50);
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.Property(u => u.PhoneNumber).HasMaxLength(30);
            });

            // Configure Service
            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(s => s.Description)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(s => s.Price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(s => s.IsAvailable)
                      .HasDefaultValue(true);
            });

            // Configure Appointment relationships and enum storage
            modelBuilder.Entity<Appointment>(entity =>
            {
                // Appointment -> Customer (required)
                entity.HasOne(a => a.Customer)
                      .WithMany()
                      .HasForeignKey(a => a.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Appointment -> ServiceProvider (optional)
                entity.HasOne(a => a.ServiceProvider)
                      .WithMany()
                      .HasForeignKey(a => a.ServiceProviderId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Appointment -> Service (required)
                entity.HasOne(a => a.Service)
                      .WithMany()
                      .HasForeignKey(a => a.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Store Status enum as string for readability and set default
                entity.Property(a => a.Status)
                      .HasConversion<string>()
                      .HasDefaultValue(StatusType.Pending);

                entity.Property(a => a.DateAndTime)
                      .IsRequired();
            });

            // other model configuration...
        }
    }
}
