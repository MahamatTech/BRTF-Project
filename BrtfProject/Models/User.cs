using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class User
    {
        public User()
        {
            Purge = false;

            Bookings = new HashSet<Booking>();

            Reservations = new HashSet<Reservation>();
        }

        public int ID { get; set; }

        [Display(Name = "User Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [Display(Name = "Formal Name")]
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        [Display(Name = "Student #")]
        [Required(ErrorMessage = "You cannot leave the student number blank.")]
        [StringLength(50, ErrorMessage = "The student number cannot be more than 50 characters long.")]
        public string StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Term and Program")]
        [Required(ErrorMessage = "You cannot leave the term and/or program blank.")]
        [StringLength(50, ErrorMessage = "Term and/or program name cannot be more than 50 characters long.")]
        public string Term { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Purge { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
