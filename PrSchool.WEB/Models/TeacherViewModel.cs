using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrSchool.WEB.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstMidlName { get; set; }
        [Required]
        public string Post { get; set; }


        public virtual ICollection<ClassViewModel> Classes { get; set; }
        public TeacherViewModel()
        {
            Classes = new List<ClassViewModel>();
        }
    }
}