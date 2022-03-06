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
        public int UserId{ get; set; }
        public User User { get; set; }



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

        [Display(Name = "Room")]
        [Required(ErrorMessage = "You must select the Room")]
        public int RoomID { get; set; }
        public Room Room { get; set; }



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

        public int AreaId { get; set; }
        public Area Area { get; set; }


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
