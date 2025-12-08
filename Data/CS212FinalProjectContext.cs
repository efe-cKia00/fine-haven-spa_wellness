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

        public DbSet<CS212FinalProject.Models.User> User { get; set; } = default!;
    }
}
