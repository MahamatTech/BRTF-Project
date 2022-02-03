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
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<RoomRules> RoomRules { get; set; }

        public DbSet<RepeatRoom> RepeatRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<ReservationRoomDetails> ReservationRoomDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.StudentID)
            .IsUnique();

            

            modelBuilder.Entity<User>()
                .HasMany<Booking>(d => d.Bookings)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasMany<Reservation>(d => d.Reservations)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany<Reservation>(d => d.Reservations)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            


        }
    }
}