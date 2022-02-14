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

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomRules> RoomRules { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Schema for Azure
            //modelBuilder.HasDefaultSchema("BR");
            modelBuilder.Entity<User>()
            .HasIndex(u => u.StudentID)
            .IsUnique();

                 

            


        }
    }
}