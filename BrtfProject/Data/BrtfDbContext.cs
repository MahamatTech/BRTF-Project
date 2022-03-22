using BrtfProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BrtfProject.Data
{
      public class BrtfDbContext : DbContext
       {
        //public BrtfDbContext(DbContextOptions<BrtfDbContext> options, IHttpContextAccessor httpContextAccessor)
        //     : base(options)
        //{
        //}public class BrtfDbContext : DbContext

        public BrtfDbContext(DbContextOptions<BrtfDbContext> options)
             : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomRules> RoomRules { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<ProgramTerm> ProgramTerms { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<FunctionalRules> FunctionalRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        




            //Add a unique index to the City/Province
            modelBuilder.Entity<Room>()
            .HasIndex(r => new { r.ID, r.IsEnable, r.name ,r.capacity,r.AreaId })
            .IsUnique();

            //Add this so you don't get Cascade Delete
            modelBuilder.Entity<Area>()
                .HasMany<Room>(r => r.Rooms)
                .WithOne(a => a.Area)
                .HasForeignKey(a => a.AreaId)
                .OnDelete(DeleteBehavior.Restrict);
            //Schema for Azure
            //modelBuilder.HasDefaultSchema("BR");
            modelBuilder.Entity<User>()
            .HasIndex(u => u.StudentID)
            .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Booking>(d => d.Bookings)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Term>()
                .HasMany<User>(u => u.Users)
                .WithOne(t => t.Term)
                .HasForeignKey(t => t.TermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>()
                .HasMany(a => a.RoomRules)
                .WithOne(r => r.Area)
                .HasForeignKey(r => r.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>()
                .HasOne(a => a.FunctionalRules)
                .WithOne(f => f.Area);

            modelBuilder.Entity<InputModel>()
                .HasNoKey();


            modelBuilder.Entity<Room>()
                .HasMany<Booking>(d => d.Bookings)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}