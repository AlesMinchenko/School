using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.DAL.Entities
{
    public class Pupil
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidlName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public int? ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
