﻿// <auto-generated />
using System;
using CanadaGames.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BrtfProject.Data.BRMigrations
{
    [DbContext(typeof(BrtfDbContext))]
    partial class BrtfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21");

            modelBuilder.Entity("BrtfProject.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reservation")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartdateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BrtfProject.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<bool>("IsEnable")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RepeatEndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("capacity")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BrtfProject.Models.RoomRules", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RuleDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomRules");
                });

            modelBuilder.Entity("BrtfProject.Models.Room_usage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("Program")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<int?>("RoomID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Term")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Usertype")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("RoomID");

                    b.ToTable("Room_usage");
                });

            modelBuilder.Entity("BrtfProject.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<bool>("Purge")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentID")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Term")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("StudentID")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BrtfProject.Models.Booking", b =>
                {
                    b.HasOne("BrtfProject.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BrtfProject.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BrtfProject.Models.RoomRules", b =>
                {
                    b.HasOne("BrtfProject.Models.Room", null)
                        .WithMany("RoomRules")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BrtfProject.Models.Room_usage", b =>
                {
                    b.HasOne("BrtfProject.Models.Room", null)
                        .WithMany("Room_usages")
                        .HasForeignKey("RoomID");
                });
#pragma warning restore 612, 618
        }
    }
}
