using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class ProgramTerm
    {
        public ProgramTerm()
        {
            Users = new HashSet<User>();

        }
        public int ID { get; set; }

        [Display(Name = "Program Description")]
        [Required(ErrorMessage = "You cannot leave the program info blank.")]
        public string ProgramInfo { get; set; }

        [Display(Name = "Program Code")]
        [Required(ErrorMessage = "You cannot leave the program code blank.")]
        [StringLength(50, ErrorMessage = "Program code cannot be more than 50 characters long.")]
        public string ProgramCode { get; set; }

        [Required]
        [Display(Name = "User Group")]
        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
