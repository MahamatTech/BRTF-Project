using BrtfProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpecialNote { get; set; }
        public string StartLevel { get; set; }
        public string LastLevel { get; set; }
        public string [] Description => new string[] { "Edit 13", "Edit 15", "Edit 6", "Edit 8 V204i" };
        public string[] Type => new string[] { "internal", "external" };
        public string[] RepeatType => new string[] { "None", "Daily", "weakly", "Monthly","Yearly"};
        public DateTime RepeatEndDate { get; set; }
       // public DateTime RepeatEndDate => RepeatEndDate.ToString("MM/dd/yyyy");
        public string[] Areas => new string[] { };

        [NotMapped]
        public virtual List<Room> Rooms => new ApplicationDbContext().Room.ToList();

        public virtual List<Room_Usage> Usages => new ApplicationDbContext().Room_Usage.ToList().Where(x => x.Id == Id).ToList();




    }
}
