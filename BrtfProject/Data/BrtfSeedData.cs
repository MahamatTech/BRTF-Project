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
                        AreaName = "Edit 13 BRTF1435 & 3Yr TV",
                        IsEnabled = true,


                    },
                    new Area
                    {
                        AreaName = "Edit 15 BRTF1435, Term 5",
                        IsEnabled = true


                    },
                         new Area
                         {
                             AreaName = "Edit 6 3rd Year only",
                             IsEnabled = true


                         },
                    new Area
                    {
                        AreaName = "Edit 9, 10 & 14 2nd Years",
                        IsEnabled = true


                    }, new Area
                    {
                        AreaName = "Film Studio V001",
                        IsEnabled = true


                    },
                         new Area
                         {
                             AreaName = "Edits 1-5 3rd Year Film",
                             IsEnabled = true


                         },
                    new Area
                    {
                        AreaName = "Green Room",
                        IsEnabled = true


                    }, new Area
                    {
                        AreaName = "MAC Lab V106",
                        IsEnabled = true


                    },
                         new Area
                         {
                             AreaName = "MAC Lab V106",
                             IsEnabled = true


                         },
                    new Area
                    {
                        AreaName = "",
                        IsEnabled = true


                    },
                     new Area
                     {
                         AreaName = "...",
                         IsEnabled = true


                     },
                    new Area
                    {
                        AreaName = "",
                        IsEnabled = true


                    },
                     new Area
                     {
                         AreaName = "",
                         IsEnabled = true


                     },
                    new Area
                    {
                        AreaName = "Radio Edit Suites V109",
                        IsEnabled = true


                    }, new Area
                    {
                        AreaName = "",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "",
                        IsEnabled = true


                    }, new Area
                    {
                        AreaName = "Edit 13 BRTF1435 & 3Yr TV",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "Edit 15 BRTF1435, Term 5",
                        IsEnabled = true


                    }, new Area
                    {
                        AreaName = "Edit 13 BRTF1435 & 3Yr TV",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "Edit 15 BRTF1435, Term 5",
                        IsEnabled = true


                    }

                    );
                    context.SaveChanges();
                }


                // Look for any Patients.  Since we can't have patients without Doctors.
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(


                    new Room
                        {  
                        name = "Edit 13  ",
                        description = "This Suite Contains: Media Composer, Adobe Suite, DaVinci Resolve, Pro Tools",
                        capacity = "20",
                        EMail = "Room@room.com",
                        AreaId= context.Areas.FirstOrDefault(a => a.AreaName == "Edit 13 BRTF1435 & 3Yr TV" && a.IsEnabled == true).ID


                    },
                     new Room
                     {   
                         name = "Edit 15",
                         description = "Media Composer, Pro Tools, DaVinci Resolve, Creative Suite",
                         capacity = "30",
                         EMail = "Room@room.com",
                         AreaId = context.Areas.FirstOrDefault(a => a.AreaName == "Edit 6 3rd Year only" && a.IsEnabled == true).ID

                     },

                    new Room
                    {
                        AreaId = 3,
                        name = "Edit 6",
                        description = "Media Composer, Pro Tools, Resolve and Creative Suite",
                        capacity = "20",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 4,
                        name = "Edit 9",
                        description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        capacity = "10",
                        EMail = "Room4@room.com"
                    },
                    new Room
                    {
                        AreaId = 5,
                        name = "Edit 10",
                        description = "You can book UP TO 6 hours at a time.",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 6,
                        name = "Edit 14",
                        description = "Suites are bookable by 2nd Year students and 3rd year Presentation students.",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 7,
                        name = "Edit 1/2 Colour Suites",
                        description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 8,
                        name = "Edit 3 (Disabled)",
                        description = "Suites are restricted to 3RD YEAR FILM STUDENTS Only.",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 9,
                        name = "Edit 4 (Disabled)",
                        description = "All others will not be approved without a signed building pass.",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 10,
                        name = "Edit 5",
                        description = "You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    { 
                        AreaId = 11,
                        name = "Film Studio V001",
                        description = "no data",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 12,
                        name = "Green Room",
                        description = "Ready Room typically for those that are preparing for a TV or Film shoot",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                    new Room
                    {
                        AreaId = 13,
                        name = "Computer 1",
                        description = "Max Booking 6-hours",
                        capacity = "10",
                        EMail = "Room@room.com"
                    },
                     new Room
                     {
                         AreaId = 14,
                         name = "Computer 2",
                         description = "All MACs Contain",
                         capacity = "10",
                         EMail = "Room@room.com"
                     },
                      new Room
                      {
                          AreaId = 15,
                          name = "..",
                          description = "MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                          capacity = "10",
                          EMail = "Room@room.com"
                      },
                       new Room
                       {
                           AreaId = 16,
                           name = "Computer 17",
                           description = "17 computers",
                           capacity = "10",
                           EMail = "Room@room.com"
                       },
                        new Room
                        {
                            AreaId = 17,
                            name = "Mixing Theatre V5",
                            description = "Booking is only available after classes until midnight Monday to Friday. Weekends are OFF LIMITS.",
                            capacity = "10",
                            EMail = "Room@room.com"
                        },
                         new Room
                         {
                             AreaId = 18,
                             name = "",
                             description = "maximum booking is 8 hours",
                             capacity = "10",
                             EMail = "Room@room.com"
                         },
                          new Room
                          {
                              AreaId = 19,
                              name = "",
                              description = "Special approval must be acquired from Luke Hutton before use.",
                              capacity = "10",
                              EMail = "Room@room.com"
                          },
                           new Room
                           {
                               AreaId = 20,
                               name = "Audio Edit #1",
                               description = "Edit computers have a maximum booking time of 4 hours at a time.",
                               capacity = "10",
                               EMail = "Room@room.com"
                           },
                            new Room
                            {
                                AreaId = 21,
                                name = "Audio Edit #2",
                                description = "8 audio edits",
                                capacity = "10",
                                EMail = "Room@room.com"
                            },
                             new Room
                             {
                                 AreaId = 22,
                                 name = "Audio Edit #8",
                                 description = "",
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
                        EndDateTime = DateTime.Parse("1955-09-01"),

                        

                    });

                }

            }

        }
    
}
}

   