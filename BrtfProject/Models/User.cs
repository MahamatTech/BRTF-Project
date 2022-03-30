using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class InputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class User : IValidatableObject
    {
        public User()
        {
            Purge = false;

            Bookings = new HashSet<Booking>();
        }

        public int ID { get; set; }

        [Display(Name = "User Name")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }
        [Display(Name = "Formal Name")]
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? "" :
                        (" " + (char?)MiddleName[0] + ".").ToUpper());
            }
        }

        [Display(Name = "Student #")]
        [Required(ErrorMessage = "You cannot leave the student number blank.")]
        [Range(1, 2147483648, ErrorMessage = "The student number cannot be a negative or 0.")]
        public int StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Last Level")]
        public bool LastLevel { get; set; }
        
        [Display(Name = "Student Level")]
        [Required(ErrorMessage = "You must input an integer for the student level.")]
        public int TermLevel { get; set; }

        [Display(Name = "ProgramTerm")]
        public int ProgramTermId { get; set; }
        public ProgramTerm ProgramTerm { get; set; }

        [Display(Name = "ProgramTerm")]
        public int TermId { get; set; }
        public Term Term { get; set; }

        [Required(ErrorMessage = "Email Address is required, you cannot leave it blank.")]
        [StringLength(255, ErrorMessage = "Email address is too lengthy, must be less than 255 characters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Purge { get; set; }

        [Required]
        [Display(Name = "User Group")]
        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        

        [NotMapped]
        public InputModel Input { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Email.Contains("@ncstudents.niagaracollege.ca") || !Email.Contains("niagaracollege.ca"))
            {
                yield return new ValidationResult("Email must be from niagara college, using @ncstudents.niagaracollege.ca or @niagaracollege.ca", new[] { "Email" });
            }
        }
    }
}



