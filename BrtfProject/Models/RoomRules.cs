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
        public int RoomId { get; set; }

        [Display(Name = "Rule Name")]
        [Required(ErrorMessage = "You cannot leave the room name blank.")]
        [StringLength(100, ErrorMessage = "Room name cannot be more than 100 characters long.")]
        public string RuleName { get; set; }

        [Display(Name = "Room Name")]
        public string RuleDescription { get; set; }
        
    }
}
