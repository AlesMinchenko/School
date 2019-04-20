using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.DAL.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidlName { get; set; }
        public string Post { get; set; }


        public virtual ICollection<Class> Classes { get; set; }
        public Teacher()
        {
            Classes = new List<Class>();
        }
    }
}
