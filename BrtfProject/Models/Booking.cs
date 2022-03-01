using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrtfProject.Models;


namespace BrtfProject.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }


        [Display(Name = "UserName")]
        public int UserId{ get; set; }


        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                    (" " + (char?)MiddleName[0] + ".").ToUpper())
                    + LastName;
            }
        }

        [Display(Name = "RoomName")]
        [Required(ErrorMessage = "You cannot leave the room name blank.")]
        [StringLength(100, ErrorMessage = "Too Big!")]
        public string RoomName { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "You cannot leave the FirstName blank.")]
        [StringLength(50, ErrorMessage = "FirstName can not be more than 50 characteres long!")]
        public string FirstName { get; set; }

        [Display(Name = "MiddleName")]
        [Required(ErrorMessage = "You cannot leave the MiddleName blank.")]
        [StringLength(50, ErrorMessage = "MiddleName can not be more than 50 characteres long!")]
        public string MiddleName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "You cannot leave the LastName blank.")]
        [StringLength(50, ErrorMessage = "LastName can not be more than 50 characteres long!")]
        public string LastName { get; set; }


        [Display(Name = "SpecialNote")]
        public string SpecialNote { get; set; }



        [Required(ErrorMessage = "You cannot leave the StartdateTime blank.")]
        [Display(Name = "StartdateTime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartdateTime { get; set; }


        [Required(ErrorMessage = "End date must be greater than start dat.")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndDateTime { get; set; }


        [Required(ErrorMessage = "Email Address is required")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Area")]
        [Required(ErrorMessage = "You cannot leave the Area name blank.")]
        [StringLength(50, ErrorMessage = "Area name cannot be more than 50 characters long.")]
        public string AreaName { get; set; }


        [Display(Name = "Repeat")]


        public bool IsEnabled { get; set; }

        // [Display(Name = "End Date")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[MyDate(ErrorMessage = "Back date entry not allowed")]
        //[DateGreaterThanAttribute(otherPropertyName = "StartDate", ErrorMessage = "End date must be greater than start date")]
        // DateTime? EndDate { get; set; }




        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Bck Date Entry is not allowed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime RepeatEndDateTime { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDateTime.Value < DateTime.Today)
            {
                yield return new ValidationResult("End Date time can not be in the past", new[] { "EndDateTime" });
            }




        }
    }
}
