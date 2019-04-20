using PrSchool.DAL.EF;
using PrSchool.DAL.Entities;
using PrSchool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private PrSchoolContext db;

        private RepositoryClass repositoryClass;
        private RepositoryPupil repositoryPupil;
        private RepositorySubject repositorySubject;
        private RepositoryTeacher repositoryTeacher;

        public EFUnitOfWork(/*string connectionString*/)
        {
            db = new PrSchoolContext();
        }

        public IRepository<Class> Clases
        {
            get
            {
                if (repositoryClass == null)
                {
                    repositoryClass = new RepositoryClass(db);
                }
                return repositoryClass;
            }
        }
        public IRepository<Pupil> Pupils
        {
            get
            {
                if (repositoryPupil == null)
                {
                    repositoryPupil = new RepositoryPupil(db);
                }
                return repositoryPupil;
            }
        }
        public IRepository<Subject> Subjects
        {
            get
            {
                if (repositorySubject == null)
                {
                    repositorySubject = new RepositorySubject(db);
                }
                return repositorySubject;
            }
        }
        public IRepository<Teacher> Teachers
        {
            get
            {
                if (repositoryTeacher == null)
                {
                    repositoryTeacher = new RepositoryTeacher(db);
                }
                return repositoryTeacher;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
