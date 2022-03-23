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
        public int id { get; set; }
        public int MaxHours { get; set; }
        //Hidden Connection
        public int AreaId { get; set; }
        public Area Area { get; set; }


        //public int MaxBookings { get; set; }

        //[DataType(DataType.Time)]
        //public DateTime StartHour { get; set; }

        //[DataType(DataType.Time)]
        //public DateTime EndHour { get; set; }

    }
}
