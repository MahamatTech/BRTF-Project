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
        

        [Display(Name = "Rule Name")]
        [Required(ErrorMessage = "You cannot leave the rule name blank.")]
        [StringLength(100, ErrorMessage = "Rule name cannot be more than 100 characters long.")]
        public string RuleName { get; set; }

        [Display(Name = "Rule Description")]
        [Required(ErrorMessage = "You cannot leave the rule description blank.")]
        [StringLength(100, ErrorMessage = "Rule description cannot be more than 500 characters long.")]
        public string RuleDescription { get; set; }

        [Display(Name = "Room Name")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}
