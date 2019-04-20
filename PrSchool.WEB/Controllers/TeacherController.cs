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
    public class TeacherController : Controller
    {
        ISchoolService schoolService;
        public TeacherController(ISchoolService school)
        {
            schoolService = school;
        }

        public ActionResult Index()
        {
            IEnumerable<TeacherDTO> teacherDTOs = schoolService.GetTeachers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            var classes = mapper.Map<IEnumerable<TeacherDTO>, List<TeacherViewModel>>(teacherDTOs);

            return View(classes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherDTOs = schoolService.GetTeacher(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            var teacher = mapper.Map<TeacherDTO, TeacherViewModel>(teacherDTOs);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

         [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            ViewBag.Subject = new SelectList(schoolService.GetSubjects(), "Id", "Name");
            ViewBag.Post = new SelectList(new[] { "Директор", "Завуч", "Учитель" });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstMidlName,Post,SubjectId")] TeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherViewModel, TeacherDTO>()).CreateMapper();
                var _teacher = mapper.Map<TeacherViewModel, TeacherDTO>(teacher);

                schoolService.Create(_teacher);
                return RedirectToAction("Index");
            }

            // ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", teacher.SubjectId);
            return View(teacher);
        }

         [Authorize(Users = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherDtos = schoolService.GetTeacher(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            var teacher = mapper.Map<TeacherDTO, TeacherViewModel>(teacherDtos);

            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Post = new SelectList(new[] { "Директор", "Завуч", "Учитель" });
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstMidlName,Post")] TeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherViewModel, TeacherDTO>()).CreateMapper();
                var _teacher = mapper.Map<TeacherViewModel, TeacherDTO>(teacher);
                schoolService.Edit(_teacher);
                return RedirectToAction("Index");
            }
            ViewBag.Post = new SelectList(new[] { "Директор", "Завуч", "Учитель" }, "Учитель");
            return View(teacher);
        }

        [Authorize(Users = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teacherDtos = schoolService.GetTeacher(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            var teacher = mapper.Map<TeacherDTO, TeacherViewModel>(teacherDtos);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var teacherDtos = schoolService.GetTeacher(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, TeacherViewModel>()).CreateMapper();
            var teacher = mapper.Map<TeacherDTO, TeacherViewModel>(teacherDtos);

            schoolService.DeleteT(teacher.Id);
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