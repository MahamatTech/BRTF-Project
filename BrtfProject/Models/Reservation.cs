using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Reservation
    {
        public int id { get; set; }

        [Required(ErrorMessage = "You must enter the start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "You must enter the End date.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Reserved by name")]
        [Required(ErrorMessage = "You cannot leave the reserved by blank.")]
        [StringLength(50, ErrorMessage = "Reserved by name cannot be more than 50 characters long.")]
        public string ReservedBy { get; set; }
        

        
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<ReservationRoomDetails> ReservationRoomDetails { get; set; }
    }
}
