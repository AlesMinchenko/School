using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrSchool.WEB.Models
{
    public class PupilViewModel
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstMidlName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public int? ClassId { get; set; }
        public virtual ClassViewModel Class { get; set; }
    }
}