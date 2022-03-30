using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrtfProject.Models;


namespace BrtfProject.Models
{
    public class Booking : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "Users")]
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

        public int AreaId { get; set; }
        public Area Area { get; set; }



        [Display(Name = "Room")]
        [Required(ErrorMessage = "You must select the Room")]
        public int RoomID { get; set; }
        public Room Room { get; set; }



        [Display(Name = "FirstName")]
        //[Required(ErrorMessage = "You cannot leave the FirstName blank.")]
        [StringLength(50, ErrorMessage = "FirstName can not be more than 50 characteres long!")]
        public string FirstName { get; set; }

        [Display(Name = "MiddleName")]
        //[Required(ErrorMessage = "You cannot leave the MiddleName blank.")]
        [StringLength(50, ErrorMessage = "MiddleName can not be more than 50 characteres long!")]
        public string MiddleName { get; set; }

        [Display(Name = "LastName")]
        //[Required(ErrorMessage = "You cannot leave the LastName blank.")]
        [StringLength(50, ErrorMessage = "LastName can not be more than 50 characteres long!")]
        public string LastName { get; set; }


        [Display(Name = "SpecialNote")]
        public string SpecialNote { get; set; }



        [Required(ErrorMessage = "You cannot leave the StartdateTime blank.")]
        [Display(Name = "Start Date Time")]
        [DataType(DataType.DateTime)]
       // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartdateTime { get; set; }


        
        [Required(ErrorMessage = "End date must be greater than start date.")]
        [Display(Name = "End Date Time")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDateTime { get; set; }


       
       

        // [Display(Name = "End Date")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[MyDate(ErrorMessage = "Back date entry not allowed")]
        //[DateGreaterThanAttribute(otherPropertyName = "StartDate", ErrorMessage = "End date must be greater than start date")]
        // DateTime? EndDate { get; set; }




       
       // [Display(Name = "Repeated Booking")]
        //public string RepeatedBooking { get; set; }

       

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (StartdateTime > EndDateTime)
            {
                yield return new ValidationResult("start date can not be in the future", new[] { "StartdateTime" });
            }

            if (EndDateTime.Value < StartdateTime)
            {
                yield return new ValidationResult("start date can not be greather or equal to End Date, please select new date Time", new[] { "StartdateTime" });
            }
            if (EndDateTime.Value  <= StartdateTime)
            {
                yield return new ValidationResult("End Date time can not be before or equal to Start Date", new[] { "EndDateTime" });
            }
           
        

    }

        }


    }


