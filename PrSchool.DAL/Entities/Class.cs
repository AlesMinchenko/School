using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.DAL.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

        public Class()
        {
            Pupils = new List<Pupil>();

            Teachers = new List<Teacher>();

        }
    }
}
