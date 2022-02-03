using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class ReservationRoomDetails
    {
        public int id { get; set; }

        public int ReservationId { get; set; }

        [Display(Name = "Number of rooms booked")]
        [Required(ErrorMessage = "You cannot leave the room name blank.")]
        public int NumberOfRoomsBooked { get; set; }

        [Display(Name = "Reservation status")]
        [Required(ErrorMessage = "You cannot leave the room name blank.")]
        public string ReservationStatus { get; set; }
        

    }
}
