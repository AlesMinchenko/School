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
    class RepositorySubject : IRepository<Subject>
    {
        PrSchoolContext db;

        public RepositorySubject(PrSchoolContext context)
        {
            db = context;
        }

        public IEnumerable<Subject> GetAll()
        {
            return db.Subjects;
        }

        public Subject Get(int? id)
        {
            return db.Subjects.Find(id);
        }
        public void Create(Subject subject)
        {
            db.Subjects.Add(subject);
        }
        public void Update(Subject subject)
        {
            db.Entry(subject).State = EntityState.Modified;
        }
        public IEnumerable<Subject> Find(Func<Subject, Boolean> predicate)
        {
            return db.Subjects.Where(predicate).ToList();

        }

        public void Delete(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject != null)
            {
                db.Subjects.Remove(subject);
            }
        }
    }
}
