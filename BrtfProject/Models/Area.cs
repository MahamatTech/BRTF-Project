using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Area
    {
        public Area()
        {
            Rooms = new HashSet<Room>();

        }
        public int ID { get; set; }

        [Display(Name = "Area")]
        [Required(ErrorMessage = "You cannot leave the Area name blank.")]
        [StringLength(50, ErrorMessage = "Area name cannot be more than 50 characters long.")]
        public string AreaName { get; set; }

        public bool IsEnabled { get; set; }




        public ICollection<Room> Rooms { get; set; }
    }
}
