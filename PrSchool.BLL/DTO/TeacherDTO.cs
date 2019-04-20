using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.BLL.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidlName { get; set; }
        public string Post { get; set; }

        public virtual ICollection<ClassDTO> Classes { get; set; }
        public TeacherDTO()
        {
            Classes = new List<ClassDTO>();
        }
    }
}
