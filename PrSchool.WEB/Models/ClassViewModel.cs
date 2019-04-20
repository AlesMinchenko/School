using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrSchool.WEB.Models
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }

        public virtual ICollection<PupilViewModel> Pupils { get; set; }

        public virtual ICollection<TeacherViewModel> Teachers { get; set; }

        public ClassViewModel()
        {
            Pupils = new List<PupilViewModel>();

            Teachers = new List<TeacherViewModel>();

        }
    }
}