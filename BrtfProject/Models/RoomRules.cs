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

        [Display(Name = "Rule Description")]
        [Required(ErrorMessage = "You must have a description of the rule.")]
        [StringLength(500, ErrorMessage = "Cannot have a rule description over 500 characters long.")]
        public string rule { get; set; }

        [Display(Name = "Area Name")]
        [Required]
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
