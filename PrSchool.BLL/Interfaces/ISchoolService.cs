using PrSchool.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrSchool.BLL.Interfaces
{
    public interface ISchoolService
    {
        IEnumerable<PupilDTO> GetPupils();
        IEnumerable<SubjectDTO> GetSubjects();
        IEnumerable<TeacherDTO> GetTeachers();
        IEnumerable<ClassDTO> GetClases();

        PupilDTO GetPupil(int? id);
        void Create(PupilDTO pupil);
        void Edit(PupilDTO pupil);
        void DeleteP(int? id);


        SubjectDTO GetSubject(int? id);
        void Create(SubjectDTO subject);
        void Edit(SubjectDTO subject);
        void DeleteS(int? id);


        TeacherDTO GetTeacher(int? id);
        void Create(TeacherDTO teacher);
        void Edit(TeacherDTO teacher);
        void DeleteT(int? id);


        ClassDTO GetClass(int? id);
        void Create(ClassDTO @class);
        void Edit(ClassDTO @class);
        void Delete(int? id);
        void Dispose();
    }
}
