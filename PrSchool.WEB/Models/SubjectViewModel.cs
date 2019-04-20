using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrSchool.WEB.Models
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}