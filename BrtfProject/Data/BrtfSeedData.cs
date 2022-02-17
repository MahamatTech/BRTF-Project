using BrtfProject.Data;
using BrtfProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Data
{
    public class BrtfSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new BrtfDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BrtfDbContext>>()))
            {
                if (!context.Areas.Any())
                {
                    context.Areas.AddRange(
                    new Area
                    {
                        AreaName = "30 meters",
                        Description = "Description",
                        IsEnabled = true,
                    });
                    context.SaveChanges();
                }


                // Look for any Patients.  Since we can't have patients without Doctors.
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(
                     new Room
                     {
                         AreaId = 1,
                         name = "Room1",
                         description = "big room",
                         capacity = "30",
                         EMail = "Room@room.com"
                     },

                    new Room
                    {
                        AreaId = 1,
                        name = "Room 2 ",
                        description = "big room",
                        capacity = "20",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 1,
                        name = "Room 3 ",
                        description = "small room",
                        capacity = "10",
                        EMail = "Room@room.com"
                    }
                );
                    context.SaveChanges();
                }

                if (!context.ProgramTerms.Any())
                {
                    context.ProgramTerms.AddRange(
                    new ProgramTerm
                    {
                        ProgramInfo = "music",
                        Term = "1"
                    });

                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                    new User
                    {
                        FirstName = "Fred",
                        LastName = "Flintstone",
                        StudentID = "1231231234",
                        ProgramTermId = 1,
                        Email = "fflintstone@outlook.com"

                    },
                    new User
                    {
                        FirstName = "Wilma",
                        LastName = "Flintstone",
                        StudentID = "1321321324",
                        ProgramTermId = 1,
                        Email = "wflintstone@outlook.com"
                    },
                    new User
                    {
                        FirstName = "Barney",
                        LastName = "Rubble",
                        StudentID = "3213213214",
                        ProgramTermId = 1,
                        Email = "brubble@outlook.com"
                    },
                    new User
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        StudentID = "4124124123",
                        ProgramTermId = 1,
                        Email = "jdoe@outlook.com"
                    }
                    );
                    context.SaveChanges();
                }
                if (!context.Bookings.Any())
                {
                    context.Bookings.AddRange(
                    new Booking
                    {
                        RoomId = 1,
                        UserId = 1,
                        StartdateTime = DateTime.Parse("1955-09-01"),
                        EndDateTime = DateTime.Parse("1955-09-01")

                    });

                }

            }

        }
    
}
}

   