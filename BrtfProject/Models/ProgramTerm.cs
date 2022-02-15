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

        public string ProgramInfo { get; set; }

        [Display(Name = "Term and Program")]
        [Required(ErrorMessage = "You cannot leave the term and/or program blank.")]
        [StringLength(50, ErrorMessage = "Term and/or program name cannot be more than 50 characters long.")]
        public string Term { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
