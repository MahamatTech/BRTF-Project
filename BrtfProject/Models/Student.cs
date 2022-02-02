using BrtfProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrtfProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName{ get; set; }
        public string Password { get; set; }
        public string Plan { get; set; }
        public string Term { get; set; }

        //[NotMapped]
        //public virtual List<Student> Students => new ApplicationDbContext().Student.ToList();
        public virtual List<Student> Students => new ApplicationDbContext().Student.ToList().Where(x => x.Id == Id).ToList();










    }
}

