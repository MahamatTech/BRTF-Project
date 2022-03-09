using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class RoomRules
    {

        public int id { get; set; }

        

        [Display(Name = "Start Hour")]
        [DataType(DataType.Time)]
        public DateTime StartHour { get; set; }

        [Display(Name = "End Hour")]
        [DataType(DataType.Time)]
        public DateTime EndHour { get; set; }

        [Display(Name = "Max Hour")]
        [Required]
        public int MaxHours { get; set; }

        [Display(Name = "Area Name")]
        [Required]
        public int AreaId { get; set; }
        public Area Area { get; set; }

    }
}
