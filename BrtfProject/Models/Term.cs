using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Term
    {
        public Term()
        {
            Users = new HashSet<User>();
        }
        public int ID { get; set; }
        [Display(Name = "Term Code")]
        [Required(ErrorMessage = "You cannot leave the term code blank.")]
        public int Code { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
