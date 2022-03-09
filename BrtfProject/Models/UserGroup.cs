using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class UserGroup
    {
        public UserGroup()
        {
            ProgramTerms = new HashSet<ProgramTerm>();

            Users = new HashSet<User>();

        }
        public int ID { get; set; }

        [Display(Name = "User Group")]
        [Required(ErrorMessage = "You cannot leave the user group blank.")]
        [StringLength(50, ErrorMessage = "Area name cannot be more than 50 characters long.")]
        public string UserGroupName { get; set; }

        public ICollection<ProgramTerm> ProgramTerms { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
