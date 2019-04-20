using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.BLL.DTO
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        public virtual ICollection<PupilDTO> Pupils { get; set; }
        public virtual ICollection<TeacherDTO> Teachers { get; set; }

        public ClassDTO()
        {
            Pupils = new List<PupilDTO>();

            Teachers = new List<TeacherDTO>();

        }
    }
}
