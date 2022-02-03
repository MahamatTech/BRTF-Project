using BrtfProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CanadaGames.Data
{
    public class BrtfDbContext : DbContext
    {
        public BrtfDbContext(DbContextOptions<BrtfDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.StudentID)
            .IsUnique();
        }
    }
}