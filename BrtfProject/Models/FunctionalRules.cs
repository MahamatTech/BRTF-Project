using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class FunctionalRules
    {
        [ForeignKey("Area")]
        public int id { get; set; }

        [Display(Name = "Max Hour")]
        public int MaxHours { get; set; }

        [Display(Name = "Start Hour")]
        [DataType(DataType.Time)]
        public DateTime StartHour { get; set; }

        [Display(Name = "End Hour")]
        [DataType(DataType.Time)]
        public DateTime EndHour { get; set; }

        //Hidden Connection
        public virtual Area Area { get; set; }
    }
}
