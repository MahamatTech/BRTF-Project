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
                        IsEnabled = true,

                    },
                    new Area
                    {
                        AreaName = "Edit 6 3rd Year only",
                        IsEnabled = true,
                    }, 
                    new Area
                    {
                        AreaName = "Radio Edit Suites V109",
                        IsEnabled = true,
                    },
                    new Area
                    {
                        AreaName = "Edit 8 Inside Niagara",
                        IsEnabled = true,
                       



                    },
                    new Area
                    {
                        AreaName = "Edit 9, 10 & 14 2nd Years",
                        IsEnabled = true
                    },
                     new Area
                     {
                         AreaName = "Edits 1-5 3rd Year Film",
                         IsEnabled = true,
                     },

                    new Area
                    {
                        AreaName = "Film Studio V001",
                        IsEnabled = true


                    },

                    new Area
                    {
                        AreaName = "Green Room",
                        IsEnabled = true,
 
                    }, new Area
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
                        IsEnabled = true,
                        

                    },
                    new Area
                    {
                        AreaName = "Radio Recording Studios V109",
                        IsEnabled = true,
                        

                    },
                    new Area
                    {
                        AreaName = "TV Studio V002",
                        IsEnabled = true,
                       

                    },
                    new Area
                    {
                        AreaName = "V110",
                        IsEnabled = true


                    },
                    new Area
                    {
                        AreaName = "V110 Acting Lab",
                        IsEnabled = true,
                        

                    },
                     new Area
                     {
                         AreaName = "V110f Acting Edit",
                         IsEnabled = true


                     },
                      new Area
                      {
                          AreaName = "V204p Production Planning",
                          IsEnabled = true,
                         

                      },
                       new Area
                       {
                           AreaName = "Camera Test",
                           IsEnabled = false


                       },
                        new Area
                        {
                            AreaName = "Edit 16 BRTF1435, Term 5 TV",
                            IsEnabled = false


                        },
                          new Area
                          {
                              AreaName = "MultiTrack V1j",
                              IsEnabled = false,
                              

                          },
                    new Area
                    {
                        AreaName = "V011 Assignment/Offload",
                        IsEnabled = false,
                        

                    },
                     new Area
                     {
                         AreaName = "V2 and S339 Acting",
                         IsEnabled = false,
                        

                     },
                    new Area
                    {
                        AreaName = "V3 Demonstration Lab",
                        IsEnabled = false,
                       


                    }

                    );
                    context.SaveChanges();
                }

                var AreaDictionary = context.Areas.ToDictionary(x => x.AreaName, x => x.ID);
                // Look for any Rooms.
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(


                    new Room
                    {
                        AreaId = AreaDictionary["Edit 13 BRTF1435 & 3Yr TV"],
                        name = "Edit 13",
                        description = "This Suite Contains: Media Composer, Adobe Suite, DaVinci Resolve, Pro Tools. Suites are restricted to 4th TERM FILM/TV or 5th TERM TV STUDENTS ONLY. You can book UP TO 4 hours at a time and have UP TO 3 future booking time-blocks",
                        IsEnable = true

                    },
                     new Room
                     {
                         name = "Edit 15",
                         description = "Media Composer, Pro Tools, DaVinci Resolve, Creative Suite. Suites are bookable by 4th term Film/TV students, or 5th term TV students. You can book UP TO 4 hours at a time.",
                         AreaId = AreaDictionary["Edit 15 BRTF1435, Term 5"],
                         IsEnable = true
                     },

                    new Room
                    {
                        AreaId = AreaDictionary["Edit 6 3rd Year only"],
                        name = "Edit 6",
                        description = "Media Composer, Pro Tools, Resolve and Creative Suite. You can book UP TO 6 hours at a time and UP TO 2 future bookings.",
                        IsEnable = true
                    },
                    new Room
                    {
                        AreaId = AreaDictionary["Edit 8 Inside Niagara"],
                        name = "Edit 8 V204i",
                        description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite. You can book up to 2 hours at a time. Bookable by 3rd term Presentation and 4th term TV students.",
                        IsEnable = true
                    },
                    new Room
                    {
                        AreaId = AreaDictionary["Edit 9, 10 & 14 2nd Years"],
                        name = "Edit 9",
                        description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite. You can book UP TO 6 hours at a time. Suites are bookable by 2nd Year students and 3rd year Presentation students.",
                        IsEnable = true
                    },
                    new Room
                    {
                        AreaId = AreaDictionary["Edit 9, 10 & 14 2nd Years"],
                        name = "Edit 10",
                        description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite. You can book UP TO 6 hours at a time. Suites are bookable by 2nd Year students and 3rd year Presentation students.",
                        IsEnable = true
                    },
                    new Room
                    {
                        AreaId = AreaDictionary["Edit 9, 10 & 14 2nd Years"],
                        name = "Edit 14",
                        description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite. You can book UP TO 6 hours at a time. Suites are bookable by 2nd Year students and 3rd year Presentation students.",
                        IsEnable = true
                    },
                    new Room
                    {
                        AreaId = AreaDictionary["Edits 1-5 3rd Year Film"],
                        name = "Edit 1/2 Colour Suites",
                        description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite. Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",

                        IsEnable = true

                    },
                    new Room
                    {
                        AreaId = AreaDictionary["Edits 1-5 3rd Year Film"],
                        name = "Edit 3",
                        description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite. Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",

                        IsEnable = false

                    }, new Room
                    {
                        AreaId = AreaDictionary["Edits 1-5 3rd Year Film"],
                        name = "Edit 4",
                        description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite. Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",

                        IsEnable = false

                    },
                     new Room
                     {
                         AreaId = AreaDictionary["Edits 1-5 3rd Year Film"],
                         name = "Edit 5",
                         description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite. Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",

                         IsEnable = true

                     },


                      new Room
                      {
                          AreaId = AreaDictionary["Film Studio V001"],
                          name = "Film Studio V001",
                          IsEnable = true
                      },
                       new Room
                       {
                           AreaId = AreaDictionary["Green Room"],
                           name = "Green Room",
                           description = "Ready Room typically for those that are preparing for a TV or Film shoot. Max bookable time is 12 hours",

                           IsEnable = true
                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 1",
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers",
                           capacity = 17,

                           IsEnable = true,


                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 2",
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers",
                           capacity = 17,

                           IsEnable = true,

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 3",
                           IsEnable = true,
                           capacity = 17,

                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 4",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 5",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 6",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 7",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 8",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 9",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 10",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 11",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 12",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 13",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 14",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 15",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"],
                           name = "Computer 16",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MAC Lab V106"], 
                           name = "Computer 17",
                           IsEnable = true,
                           capacity = 17,
                           description = "Max Booking 6-hours. All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools. 17 computers"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Mixing Theatre V105"],
                           name = "Mixing Theatre V5",
                           IsEnable = true,
                           capacity = 17,
                           description = "Booking is only available after classes until midnight Monday to Friday. Weekends are OFF LIMITS. maximum booking is 8 hours. Special approval must be acquired from Luke Hutton before use."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #1",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #2",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #3",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #4",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #5",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #6",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #7",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Edit Suites V109"],
                           name = "Audio Edit #8",
                           IsEnable = true,
                           capacity = 17,
                           description = "Edit computers have a maximum booking time of 4 hours at a time. 8 audio edits"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Studio & Talk A",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Studio B",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Studio C",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Studio D",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Annc. 2",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #1",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #2",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #3",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #4",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #5",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #6",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #7",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Radio Recording Studios V109"],
                           name = "Audio Edit #8",
                           IsEnable = true,
                           capacity = 17,
                           description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports. Announce Booth 2 used for Voice Tracking. You can book up to 2 hours in a studio"

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["TV Studio V002"],
                           name = "V2 TV Studio",
                           IsEnable = true,
                           capacity = 17,
                           description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson. Max booking available is 18 hours. V2 TV Studio, Max Bookable Hours 2."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["TV Studio V002"],
                           name = "V2 GreenRoom",
                           IsEnable = true,
                           capacity = 17,
                           description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson. Max booking available is 18 hours. V2 GreenRoom, Max Bookable Hours 6"
                       },
                       new Room
                       {
                           AreaId = AreaDictionary["TV Studio V002"],
                           name = "V1 (Old Studio)",
                           IsEnable = true,
                           capacity = 17,
                           description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson. Max booking available is 18 hours. V1 (Old Studio), Max Bookable Hours 2."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["TV Studio V002"],
                           name = "TV Studio Control Room",
                           IsEnable = true,
                           capacity = 17,
                           description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson. Max booking available is 18 hours. TV Studio Control Room, Upstairs Control Room, Max Bookable Hours 2."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V110 Acting Lab"],
                           name = "Acting Lab V110",
                           IsEnable = true,
                           capacity = 17,
                           description = "You can book UP TO 2 hours at a time. Booking is OFF LIMITS from 12:30am to the end of classes Mon-Fri. For exceptions, approval must be granted by Lori Ravensborg."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V110 Acting Lab"],
                           name = "V110g Acting Edit",
                           IsEnable = true,
                           capacity = 17,
                           description = "You can book UP TO 2 hours at a time. Booking is OFF LIMITS from 12:30am to the end of classes Mon-Fri. For exceptions, approval must be granted by Lori Ravensborg."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V204p Production Planning"],
                           name = "V204p Production Planning",
                           IsEnable = true,
                           capacity = 17,
                           description = "Booking is only available Mon-Friday between 8:30am to 5:30pm. BRTF project meeting room. You can book up to 1 hour."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Camera Test"],
                           name = "Red Camera 1",
                           IsEnable = true,
                           capacity = 17,
                           description = "Assignment finishing (as opposed to interrupting Mac lab classes) and footage offload before returning your camera media to the Equipment Room. Open Access space for finishing or media transfer. Not bookable for meetings."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["Edit 16 BRTF1435, Term 5 TV"],
                           name = "Edit 16 Avid/P2/DigLotPrt",
                           IsEnable = true,
                           capacity = 17,
                           description = "This Suites Contains: P2 Reader, Digitize/Log/Print Deck, SoundTrack, Avid, Final Cut Pro, DiffMerge, Adobe CS Suite, Aspera Connect. Suites are restricted to 4th TERM FILM/TV or 5th TERM TV STUDENTS ONLY. You can book UP TO 4 hours at a time."
                       },
                       new Room
                       {
                           AreaId = AreaDictionary["MultiTrack V1j"],
                           name = "MultiTrack V1j",
                           IsEnable = true,
                           capacity = 17
                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V011 Assignment/Offload"],
                           name = "V011 Assignment/Offload",
                           IsEnable = true,
                           capacity = 17,
                           description = "Assignment finishing (as opposed to interrupting Mac lab classes) and footage offload before returning your camera media to the Equipment Room. Open Access space for finishing or media transfer. Not bookable for meetings."
                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V2 and S339 Acting"],
                           name = "V2 Acting",
                           IsEnable = true,
                           capacity = 17,
                           description = "You can book UP TO 1 hour at a time."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V2 and S339 Acting"],
                           name = "S339",
                           IsEnable = true,
                           capacity = 17,
                           description = "You can book UP TO 1 hour at a time."

                       },
                       new Room
                       {
                           AreaId = AreaDictionary["V3 Demonstration Lab"],
                           name = "V3 Demonstration Lab",
                           IsEnable = true,
                           capacity = 17,
                           description = "You can book UP TO 6 hours at a time."
                       }


                );
                    context.SaveChanges();
                }

                if (!context.UserGroups.Any())
                {
                    context.UserGroups.AddRange(
                    new UserGroup
                    {
                        UserGroupName = "Admin"
                    },
                    new UserGroup
                    {
                        UserGroupName = "Student"
                    });
                    context.SaveChanges();
                }

                if (!context.Terms.Any())
                {
                    context.Terms.AddRange(
                    new Term
                    {
                        Code = 1214
                    },
                    new Term
                    {
                        Code = 1204
                    });
                    context.SaveChanges();
                }

                if (!context.ProgramTerms.Any())
                {
                    context.ProgramTerms.AddRange(
                    new ProgramTerm
                    {
                        ProgramInfo = "Broadcasting: Radion, TV & Film",
                        ProgramCode = "PO122",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Presentation / Radio",
                        ProgramCode = "PO163",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "TV Production",
                        ProgramCode = "PO164",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Film Production",
                        ProgramCode = "PO165",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Acting for TV & Film",
                        ProgramCode = "PO198",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Digital Photography",
                        ProgramCode = "PO795",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Joint BSc Game Programming",
                        ProgramCode = "P6801",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Join BA Game Design",
                        ProgramCode = "P6800",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "Game Development",
                        ProgramCode = "P0441",
                        UserGroupId = 1
                    },
                    new ProgramTerm
                    {
                        ProgramInfo = "CST – Network and Cloud Tech",
                        ProgramCode = "P0474",
                        UserGroupId = 1
                    });
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(


                        new User
                        {
                            StudentID = 0,
                            FirstName = "Admin",
                            MiddleName = "",
                            LastName = "Universal",
                            LastLevel = false,
                            TermLevel = 0,
                            ProgramTermId = 1,
                            TermId = 1,
                            Email = "admin1@outlook.com",
                            UserGroupId = 1,

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
                            FirstName = "Admin",
                            MiddleName = "",
                            LastName = "Universal",
                            SpecialNote="",

                            //ID = context.Users.FirstOrDefault(u => u.StudentID, u. ,u.FullName == "Adoum Mahamat", u).ID




                        });
                    context.SaveChanges();
                }

            }

            }

        } }
