using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public string room_ID{ get; set; }
        public int User_ID { get; set; }
        public DateTime StartdateTime { get; set; }
        public string ArrivalDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
