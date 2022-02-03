using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class RepeatRoom
    {
        public int id { get; set; }

        public int RoomId { get; set; }

        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "You must enter the End date.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
