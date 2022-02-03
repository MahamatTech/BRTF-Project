using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool IsEnable{ get; set; }
        public string capacity { get; set; }
        public string Email { get; set; }
        public string[] Description => new string[] { "Edit 13", "Edit 15", "Edit 6", "Edit 8 V204i" };
        public string[] Areas => new string[] { "Edit 13", "Edit 15", "Edit 6", "Edit 8 V204i" };
        public string[] Type => new string[] { "internal", "external" };
        public string[] RepeatType => new string[] { "None", "Daily", "weakly", "Monthly", "Yearly" };
        public DateTime RepeatEndDate { get; set; }
    }
}
