using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrtfProject.Models;


namespace BrtfProject.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

        

        [Required(ErrorMessage = "You cannot leave the date for the StartdateTime blank.")]
        [Display(Name = "StartdateTime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartdateTime { get; set; }


        [Required(ErrorMessage = "You cannot leave the date for the EndDateTime blank.")]
        [Display(Name = "EndDateTime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }



        [Required(ErrorMessage = "You cannot leave the Type of Booker blank.")]
        [Display(Name = "BookingType")]
        public string[] Type => new string[] { "internal", "external" };


        [Required(ErrorMessage = "You must select a Booker.")]
        [Display(Name = "Booker")]
        public int BookerID { get; set; }
        public ICollection<Room> Rooms { get; set; }

    }
}
