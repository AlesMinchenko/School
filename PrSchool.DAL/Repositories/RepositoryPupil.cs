using PrSchool.DAL.EF;
using PrSchool.DAL.Entities;
using PrSchool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.DAL.Repositories
{
    class RepositoryPupil : IRepository<Pupil>
    {
        PrSchoolContext db;

        public RepositoryPupil(PrSchoolContext context)
        {
            db = context;
        }

        public IEnumerable<Pupil> GetAll()
        {
            return db.Pupils;
        }

        public Pupil Get(int? id)
        {
            return db.Pupils.Find(id);
        }
        public void Create(Pupil pupil)
        {
            db.Pupils.Add(pupil);
        }
        public void Update(Pupil pupil)
        {
            db.Entry(pupil).State = EntityState.Modified;
        }
        public IEnumerable<Pupil> Find(Func<Pupil, Boolean> predicate)
        {
            return db.Pupils.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Pupil pupil = db.Pupils.Find(id);
            if (pupil != null)
            {
                db.Pupils.Remove(pupil);
            }
        }
    }
}
