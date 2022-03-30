using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrtfProject.Models;


namespace BrtfProject.Models
{
    public class Room
    {

        public Room()
        {
            Bookings = new HashSet<Booking>();
            RoomRules = new HashSet<RoomRules>();




        }
        public int ID { get; set; }

        [Display(Name = "Room Name")]
        [Required(ErrorMessage = "You cannot leave the room name blank.")]
        [StringLength(100, ErrorMessage = "Too Big!")]
        public string name { get; set; }

        [Display(Name = "Room Description")]
        public string description
        {
            get; set;
        }


        public bool IsEnable{ get; set; }


        [Required(ErrorMessage = "You cannot leave the room capacity blank.")]
        [Display(Name = "Capacity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Capacity in integer.")]
       [DisplayFormat(NullDisplayText = "Empty")]
        public int capacity { get; set; }


        public string[] Areas => new string[] { "Edit 13", "Edit 15", "Edit 6", "Edit 8 V204i" };


        public int AreaId { get; set; }
        public Area Area { get; set; }




        public ICollection<Booking> Bookings { get; set; }



        public ICollection<RoomRules> RoomRules { get; set; }


    }
}
