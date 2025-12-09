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

        // single, idiomatic DbSet name
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Persist Role enum as string and set DB default to "Customer"
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasDefaultValue(RoleType.Customer);

            // other model configuration...
        }
    }
}
