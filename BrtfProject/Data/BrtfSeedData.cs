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


                    },
                     new Area
                     {
                         AreaName = "Edit 8 Inside Niagara",
                         IsEnabled = true


                     },
                      new Area
                      {
                          AreaName = "Edit 9, 10 & 14 2nd Years",
                          IsEnabled = true


                      },
                       new Area
                       {
                           AreaName = "Edits 1-5 3rd Year Film",
                           IsEnabled = true


                       },
                        new Area
                        {
                            AreaName = "Film Studio V001",
                            IsEnabled = true


                        },
                          new Area
                          {
                              AreaName = "Green Room",
                              IsEnabled = true


                          },
                    new Area
                    {
                        AreaName = "MAC Lab V106",
                        IsEnabled = true


                    },
                     new Area
                     {
                         AreaName = "Mixing Theatre V105",
                         IsEnabled = true


                     },
                    new Area
                    {
                        AreaName = "Radio Edit Suites V109",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "Radio Recording Studios V109",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "TV Studio V002",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "V110",
                        IsEnabled = true


                    },
                     new Area
                     {
                         AreaName = "V110 Acting Lab",
                         IsEnabled = true


                     }

                    );
                    context.SaveChanges();
                }


                // Look for any Rooms.
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(


                    new Room
                    {
                        name = "Edit 13  ",
                        description = "This Suite Contains: Media Composer, Adobe Suite, DaVinci Resolve, Pro Tools",
                        capacity = "20",

                        AreaId = context.Areas.FirstOrDefault(a => a.AreaName == "Edit 13 BRTF1435 & 3Yr TV" && a.IsEnabled == true).ID


                    },
                     new Room
                     {
                         name = "Edit 15",
                         description = "Media Composer, Pro Tools, DaVinci Resolve, Creative Suite",
                         capacity = "30",

                         AreaId = context.Areas.FirstOrDefault(a => a.AreaName == "Edit 6 3rd Year only" && a.IsEnabled == true).ID

                     },

                    new Room
                    {
                        AreaId = 1,
                        name = "Edit 6",
                        description = "Media Composer, Pro Tools, Resolve and Creative Suite",
                        capacity = "20",

                    },
                    new Room
                    {
                        AreaId = 2,
                        name = "Edit 9",
                        description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        capacity = "10"

                    },
                    new Room
                    {
                        AreaId = 3,
                        name = "Edit 10",
                        description = "You can book UP TO 6 hours at a time.",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 4,
                        name = "Edit 14",
                        description = "Suites are bookable by 2nd Year students and 3rd year Presentation students.",
                        capacity = "10"

                    },
                    new Room
                    {
                        AreaId = 5,
                        name = "Edit 1/2 Colour Suites",
                        description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 6,
                        name = "Edit 3 (Disabled)",
                        description = "Suites are restricted to 3RD YEAR FILM STUDENTS Only.",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 7,
                        name = "Edit 4 (Disabled)",
                        description = "All others will not be approved without a signed building pass.",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 14,
                        name = "Edit 5",
                        description = "You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 9,
                        name = "Film Studio V001",
                        description = "no data",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 10,
                        name = "Green Room",
                        description = "Ready Room typically for those that are preparing for a TV or Film shoot",
                        capacity = "10"
                    },
                    new Room
                    {
                        AreaId = 11,
                        name = "Computer 1",
                        description = "Max Booking 6-hours",
                        capacity = "10"
                    },
                     new Room
                     {
                         AreaId = 14,
                         name = "Computer 2",
                         description = "All MACs Contain",
                         capacity = "10"
                     },
                      new Room
                      {
                          AreaId = 14,
                          name = "..",
                          description = "MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                          capacity = "10"
                      },
                       new Room
                       {
                           AreaId = 14,
                           name = "Computer 17",
                           description = "17 computers",
                           capacity = "10"
                       },
                        new Room
                        {
                            AreaId = 15,
                            name = "Mixing Theatre V5",
                            description = "Booking is only available after classes until midnight Monday to Friday. Weekends are OFF LIMITS.",
                            capacity = "10"
                        },
                         new Room
                         {
                             AreaId = 16,
                             name = "",
                             description = "maximum booking is 8 hours",
                             capacity = "10"
                         },
                          new Room
                          {
                              AreaId = 17,
                              name = "",
                              description = "Special approval must be acquired from Luke Hutton before use.",
                              capacity = "10"
                          },
                           new Room
                           {
                               AreaId = 18,
                               name = "Audio Edit #1",
                               description = "Edit computers have a maximum booking time of 4 hours at a time.",
                               capacity = "10"
                           },
                            new Room
                            {
                                AreaId = 19,
                                name = "Audio Edit #2",
                                description = "8 audio edits",
                                capacity = "10"
                            },
                             new Room
                             {
                                 AreaId = 19,
                                 name = "Audio Edit #8",
                                 description = "",
                                 capacity = "10"
                             }
                );
                    context.SaveChanges();
                }

                if (!context.ProgramTerms.Any())
                {
                    context.ProgramTerms.AddRange(
                    new ProgramTerm
                    {
                        ID = 1,
                        ProgramInfo = "music",
                        Term = "1402"
                    });

                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(


                        new User
                        {

                            StudentID = 1,
                            ID = 1,
                            FirstName = "David ",
                            MiddleName = "Smith",
                            LastName = "Obi",
                            ProgramTermId = 1,

                            Email = "adoum@outlook.com",

                            //ID = context.Users.FirstOrDefault(u => u.StudentID, u. ,u.FullName == "Adoum Mahamat", u).ID




                        });
                    context.SaveChanges();
                }
                if (!context.Bookings.Any())
                {
                    context.Bookings.AddRange(


                        new Booking
                        {

                            UserId =1,
                            RoomID=1,
                            FirstName = "David ",
                            MiddleName = "Smith",
                            LastName = "Obi",
                            SpecialNote="",
                            Email = "David@outlook.com",

                            //ID = context.Users.FirstOrDefault(u => u.StudentID, u. ,u.FullName == "Adoum Mahamat", u).ID




                        });
                    context.SaveChanges();
                }

            }

            }

        } }
