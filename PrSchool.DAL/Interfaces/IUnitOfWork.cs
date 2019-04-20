using PrSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Class> Clases { get; }
        IRepository<Pupil> Pupils { get; }
        IRepository<Subject> Subjects { get; }
        IRepository<Teacher> Teachers { get; }
        void Save();
    }
}
