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
    public class RepositoryClass : IRepository<Class>
    {
        PrSchoolContext db;

        public RepositoryClass(PrSchoolContext context)
        {
            this.db = context;
        }

        public IEnumerable<Class> GetAll()
        {
            return db.Classes;
        }

        public Class Get(int? id)
        {
            return db.Classes.Find(id);
        }
        public void Create(Class @class)
        {
            db.Classes.Add(@class);
        }
        public void Update(Class @class)
        {
            Class _class = db.Classes.Find(@class.Id);
            db.Entry(_class).CurrentValues.SetValues(@class);
            //db.Entry(@class).State = EntityState.Detached;
            
            
        }
        public IEnumerable<Class> Find(Func<Class, Boolean> predicate)
        {
            return db.Classes.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Class @class = db.Classes.Find(id);
            if (@class != null)
            {

                db.Classes.Remove(@class);
               
            }
        }
    }
}
