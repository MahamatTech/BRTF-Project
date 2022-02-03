﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BrtfProject.Models;

namespace BrtfProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BrtfProject.Models.Booking> Booking { get; set; }

        public DbSet<BrtfProject.Models.Room> Room { get; set; }

        public DbSet<BrtfProject.Models.RoomRules> RoomRules { get; set; }

        public DbSet<BrtfProject.Models.RepeatRoom> RepeatRoom { get; set; }

        public DbSet<BrtfProject.Models.Reservation> Reservation { get; set; }

        public DbSet<BrtfProject.Models.ReservationRoomDetails> ReservationRoomDetails { get; set; }

        public DbSet<BrtfProject.Models.Room_usage> Room_usage { get; set; }
       




    }
}
