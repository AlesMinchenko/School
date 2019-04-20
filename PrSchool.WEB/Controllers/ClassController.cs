using AutoMapper;
using PrSchool.BLL.DTO;
using PrSchool.BLL.Interfaces;
using PrSchool.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PrSchool.WEB.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        ISchoolService schoolService;
        public ClassController(ISchoolService school)
        {
            schoolService = school;
        }
        public ActionResult Index()
        {
            IEnumerable<ClassDTO> classDtos = schoolService.GetClases();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, ClassViewModel>()).CreateMapper();
            var classes = mapper.Map<IEnumerable<ClassDTO>, List<ClassViewModel>>(classDtos);

            return View(classes);
        }

        public ActionResult Details(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classDtos = schoolService.GetClass(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, ClassViewModel>()).CreateMapper();
            var @class = mapper.Map<ClassDTO, ClassViewModel>(classDtos);

            //ViewBag.Pupils = new SelectList(schoolService.GetPupils(), "Name", "Name");

            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            ViewBag.selectedTeacher = new SelectList(schoolService.GetTeachers(), "Id", "FirstMidlName");
            ViewBag.Subject = new SelectList(schoolService.GetSubjects(), "Name", "Name");
            ViewBag.selectedPupil = new SelectList(schoolService.GetPupils(), "Id", "LastName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Subject")] ClassViewModel @class, int? selectedTeacher, int? selectedPupil)
        {
            if (ModelState.IsValid)
            {
                var teacherDtos = schoolService.GetTeacher(selectedTeacher);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
                var teacher = mapper.Map<TeacherDTO, TeacherViewModel>(teacherDtos);
                @class.Teachers.Add(teacher);

                var pupilDtos = schoolService.GetPupil(selectedPupil);
                var mapper_ = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
                var pupil = mapper.Map<PupilDTO, PupilViewModel>(pupilDtos);
                @class.Pupils.Add(pupil);


                var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassViewModel, ClassDTO>()).CreateMapper();
                var _class = mapper.Map<ClassViewModel, ClassDTO>(@class);

                schoolService.Create(_class);
                return RedirectToAction("Index");
            }

            return View(@class);
        }

        [Authorize(Users = "admin")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classDtos = schoolService.GetClass(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, ClassViewModel>()).CreateMapper();
            var @class = mapper.Map<ClassDTO, ClassViewModel>(classDtos);

            ViewBag.selectedTeacher = new SelectList(schoolService.GetTeachers(), "Id", "FirstMidlName");
            ViewBag.Subject = new SelectList(schoolService.GetSubjects(), "Name", "Name");
            ViewBag.selectedPupil = new SelectList(schoolService.GetPupils(), "Id", "LastName");


            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Subject")] ClassViewModel @class, int selectedTeacher, int selectedPupil)
        {
            @class.Teachers.Clear();
            @class.Pupils.Clear();

            if (selectedTeacher != 0 && selectedPupil != 0)
            {
                var p = schoolService.GetPupil(selectedPupil);
                var mappe = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
                var pup = mappe.Map<PupilDTO, PupilViewModel>(p);

                @class.Pupils.Add(pup);

                var t = schoolService.GetTeacher(selectedTeacher);
                var mappe1 = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
                var tech = mappe.Map<TeacherDTO, TeacherViewModel>(t);
                @class.Teachers.Add(tech);

            }
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassViewModel, ClassDTO>()).CreateMapper();
                var _class = mapper.Map<ClassViewModel, ClassDTO>(@class);
                schoolService.Edit(_class);

                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classDtos = schoolService.GetClass(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, ClassViewModel>()).CreateMapper();
            var @class = mapper.Map<ClassDTO, ClassViewModel>(classDtos);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var classDtos = schoolService.GetClass(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, ClassViewModel>()).CreateMapper();
            var @class = mapper.Map<ClassDTO, ClassViewModel>(classDtos);

            schoolService.Delete(@class.Id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                schoolService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}