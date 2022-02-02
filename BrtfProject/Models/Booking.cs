using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Booking
    {
        public string name { get; set; }
        public string room { get; set; }
        public string rules { get; set; }
        public string specialnotes {get; set;}
        public DateTime ArrivaldateTime { get; set; }
        public string  ArrivalDateTime { get;  set; }
        public DateTime DepartureDateTime { get; set; }
       // public string ArrivalDate=> ArrivalDateTime.ToString("MM/dd/yyyy");
        //public string ArrivalTime => ArrivalDateTime.ToString("hh:mm tt");

        public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");
       // public string DepartureTime => DepartureTime.ToString("hh:mm tt");


    }
}
