using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Room
    {

        public Room()
        {
            Bookings = new HashSet<Booking>();

            Reservations = new HashSet<Reservation>();

        }
        public int ID { get; set; }

        [Display(Name = "name")]
        [Required(ErrorMessage = "You cannot leave the room name blank.")]
        [StringLength(100, ErrorMessage = "Too Big!")]
        public string name { get; set; }


        [Display(Name = "description ")]
        [Required(ErrorMessage = "You cannot leave the description blank.")]
        public string[] Description => new string[] { "Edit 13", "Edit 15", "Edit 6", "Edit 8 V204i" };
        public string description
        {
            get; set;

        }

        public bool IsEnable{ get; set; }


        [Required(ErrorMessage = "You cannot leave the room capacity blank.")]
        [Display(Name = "capacity.")]
        [StringLength(100, ErrorMessage = "Too many!")]
        [DisplayFormat(NullDisplayText = "Empty")]
        public string capacity { get; set; }


        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }


        public string[] Areas => new string[] { "Edit 13", "Edit 15", "Edit 6", "Edit 8 V204i" };
        public string[] Type => new string[] { "internal", "external" };
        public string[] RepeatType => new string[] { "None", "Daily", "weakly", "Monthly", "Yearly" };
        public DateTime RepeatEndDate { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Reservation> Reservations { get; set; }



    }
}
