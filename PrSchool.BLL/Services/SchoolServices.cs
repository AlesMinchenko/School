using AutoMapper;
using PrSchool.BLL.DTO;
using PrSchool.BLL.Interfaces;
using PrSchool.DAL.Entities;
using PrSchool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrSchool.BLL.Infrastructure;

namespace PrSchool.BLL.Services
{
    public class SchoolServices : ISchoolService
    {
        IUnitOfWork Database { get; set; }

        public SchoolServices(IUnitOfWork unit)
        {
            Database = unit;
        }

        public IEnumerable<ClassDTO> GetClases()
        {

            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Class, ClassDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Class>, List<ClassDTO>>(Database.Clases.GetAll());
        }
        public ClassDTO GetClass(int? id)
        {
            if (id == 0)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Class, ClassDTO>()).CreateMapper();
            return mapper.Map<Class, ClassDTO>(Database.Clases.Get(id.Value));

        }
        public void Create(ClassDTO @class)
        {
            if (@class == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, Class>()).CreateMapper();
            var _class = mapper.Map<ClassDTO, Class>(@class);
            Database.Clases.Create(_class);
            Database.Save();
        }
        public void Edit(ClassDTO @class)
        {
            if (@class == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, Class>()).CreateMapper();
            var _class = mapper.Map<ClassDTO, Class>(@class);
            Database.Clases.Update(_class);
            Database.Save();
        }
        public void Delete(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id класса", "");
            Database.Clases.Delete(id.Value);
            Database.Save();
        }


        public IEnumerable<PupilDTO> GetPupils()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Pupil, PupilDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Pupil>, List<PupilDTO>>(Database.Pupils.GetAll());
        }
        public PupilDTO GetPupil(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id учителя", "");
            var pupil = Database.Pupils.Get(id.Value);
            if (pupil == null)
                throw new ValidationException("Учитель не найден", "");

            return new PupilDTO { Age = pupil.Age, Birthday = pupil.Birthday, FirstMidlName = pupil.FirstMidlName, Id = pupil.Id, LastName = pupil.LastName, Sex = pupil.Sex };

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Pupil, PupilDTO>()).CreateMapper();
            //return mapper.Map<Pupil, PupilDTO>(Database.Pupils.Get(id.Value));
        }
        public void Create(PupilDTO pupil)
        {
            if (pupil == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, Pupil>()).CreateMapper();
            var _pupil = mapper.Map<PupilDTO, Pupil>(pupil);

            Database.Pupils.Create(_pupil);
            Database.Save();
        }
        public void Edit(PupilDTO pupil)
        {
            if (pupil == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, Pupil>()).CreateMapper();
            var _pupil = mapper.Map<PupilDTO, Pupil>(pupil);
            Database.Pupils.Update(_pupil);
            Database.Save();
        }
        public void DeleteP(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id класса", "");
            Database.Pupils.Delete(id.Value);
            Database.Save();
        }

        public IEnumerable<SubjectDTO> GetSubjects()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Subject>, List<SubjectDTO>>(Database.Subjects.GetAll());
        }
        public SubjectDTO GetSubject(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id предмета", "");
            var subject = Database.Subjects.Get(id.Value);
            if (subject == null)
                throw new ValidationException("Предмет не найден", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>()).CreateMapper();
            return mapper.Map<Subject, SubjectDTO>(Database.Subjects.Get(id.Value));
        }
        public void Create(SubjectDTO subject)
        {
            if (subject == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, Subject>()).CreateMapper();
            var _subject = mapper.Map<SubjectDTO, Subject>(subject);
            Database.Subjects.Create(_subject);
            Database.Save();
        }
        public void Edit(SubjectDTO subject)
        {
            if (subject == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, Subject>()).CreateMapper();
            var _subject = mapper.Map<SubjectDTO, Subject>(subject);
            Database.Subjects.Update(_subject);
            Database.Save();
        }
        public void DeleteS(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id класса", "");
            Database.Subjects.Delete(id.Value);
            Database.Save();
        }



        public IEnumerable<TeacherDTO> GetTeachers()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Teacher>, List<TeacherDTO>>(Database.Teachers.GetAll());

        }
        public TeacherDTO GetTeacher(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id учителя", "");
            var teacher = Database.Teachers.Get(id.Value);
            if (teacher == null)
                throw new ValidationException("Учитель не найден", "");
            return new TeacherDTO() { FirstMidlName = teacher.FirstMidlName, LastName = teacher.LastName, Id = teacher.Id, Post = teacher.Post };
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>()).CreateMapper();
            //return mapper.Map<Teacher, TeacherDTO>(Database.Teachers.Get(id.Value));
        }
        public void Create(TeacherDTO teacher)
        {
            if (teacher == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>()).CreateMapper();
            var _teacher = mapper.Map<TeacherDTO, Teacher>(teacher);
            Database.Teachers.Create(_teacher);
            Database.Save();
        }
        public void Edit(TeacherDTO teacher)
        {
            if (teacher == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>()).CreateMapper();
            var _teacher = mapper.Map<TeacherDTO, Teacher>(teacher);
            Database.Teachers.Update(_teacher);
            Database.Save();
        }
        public void DeleteT(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id класса", "");
            Database.Teachers.Delete(id.Value);
            Database.Save();
        }




        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
