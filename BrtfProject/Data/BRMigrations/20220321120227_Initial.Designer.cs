﻿// <auto-generated />
using System;
using BrtfProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BrtfProject.Data.BRMigrations
{
    [DbContext(typeof(BrtfDbContext))]
    [Migration("20220321120227_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21");

            modelBuilder.Entity("BrtfProject.Models.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("BrtfProject.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AreaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDateTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("RepeatedBooking")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialNote")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartdateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("AreaId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BrtfProject.Models.InputModel", b =>
                {
                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.ToTable("InputModel");
                });

            modelBuilder.Entity("BrtfProject.Models.ProgramTerm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProgramCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("ProgramInfo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserGroupId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserGroupId");

                    b.ToTable("ProgramTerms");
                });

            modelBuilder.Entity("BrtfProject.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AreaId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AreaId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BrtfProject.Models.RoomRules", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AreaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndHour")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxHours")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RoomID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartHour")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("AreaId");

                    b.HasIndex("RoomID");

                    b.ToTable("RoomRules");
                });

            modelBuilder.Entity("BrtfProject.Models.Term", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Code")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Terms");
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

                    b.Property<bool>("LastLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("ProgramTermId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Purge")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserGroupId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProgramTermId");

                    b.HasIndex("StudentID")
                        .IsUnique();

                    b.HasIndex("TermId");

                    b.HasIndex("UserGroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BrtfProject.Models.UserGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserGroupName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("BrtfProject.Models.Booking", b =>
                {
                    b.HasOne("BrtfProject.Models.Area", "Area")
                        .WithMany("Bookings")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrtfProject.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BrtfProject.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BrtfProject.Models.ProgramTerm", b =>
                {
                    b.HasOne("BrtfProject.Models.UserGroup", "UserGroup")
                        .WithMany("ProgramTerms")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BrtfProject.Models.Room", b =>
                {
                    b.HasOne("BrtfProject.Models.Area", "Area")
                        .WithMany("Rooms")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BrtfProject.Models.RoomRules", b =>
                {
                    b.HasOne("BrtfProject.Models.Area", "Area")
                        .WithMany("RoomRules")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrtfProject.Models.Room", null)
                        .WithMany("RoomRules")
                        .HasForeignKey("RoomID");
                });

            modelBuilder.Entity("BrtfProject.Models.User", b =>
                {
                    b.HasOne("BrtfProject.Models.ProgramTerm", "ProgramTerm")
                        .WithMany("Users")
                        .HasForeignKey("ProgramTermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrtfProject.Models.Term", "Term")
                        .WithMany("Users")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BrtfProject.Models.UserGroup", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}