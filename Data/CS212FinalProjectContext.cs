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
        public CS212FinalProjectContext (DbContextOptions<CS212FinalProjectContext> options)
            : base(options)
        {
        }

        // Use plural DbSet names
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<Appointment> Appointments { get; set; } = default!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Persist Role enum as string and set DB default to "Customer"
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasDefaultValue(RoleType.Customer);

            // Configure Service
            modelBuilder.Entity<Service>(entity =>
            {
                // Column type already set by attribute, but ensure here as well
                entity.Property(s => s.Price).HasColumnType("decimal(18,2)");
                entity.Property(s => s.IsAvailable).HasDefaultValue(true);
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
            });

            // other model configuration...
        }
    }
}
