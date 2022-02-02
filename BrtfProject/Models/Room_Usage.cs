using BrtfProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Room_Usage
    {
        public int Id { get; set; }
        public int Room_Id { get; set; }
        public int Student_Id { get; set; }
        public string StartLevel { get; set; }
        public string LastLevel { get; set; }

        public virtual Room Room => new ApplicationDbContext().Room.Find(Room_Id);
        public virtual Student Student => new ApplicationDbContext().Student.Find(Student_Id);

    }
}
