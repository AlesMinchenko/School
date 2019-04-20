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
    public class SubjectController : Controller
    {
        ISchoolService schoolService;

        public SubjectController(ISchoolService school)
        {
            schoolService = school;
        }
        public ActionResult Index()
        {
            IEnumerable<SubjectDTO> subjectDTOs = schoolService.GetSubjects();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subjects = mapper.Map<IEnumerable<SubjectDTO>, List<SubjectViewModel>>(subjectDTOs);
            return View(subjects);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subjectDTOs = schoolService.GetSubject(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subject = mapper.Map<SubjectDTO, SubjectViewModel>(subjectDTOs);

            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectViewModel, SubjectDTO>()).CreateMapper();
                var _subject = mapper.Map<SubjectViewModel, SubjectDTO>(subject);

                schoolService.Create(_subject);
                return RedirectToAction("Index");
            }

            return View(subject);
        }
        [Authorize(Users = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subjectDtos = schoolService.GetSubject(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subject = mapper.Map<SubjectDTO, SubjectViewModel>(subjectDtos);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectViewModel, SubjectDTO>()).CreateMapper();
                var _subject = mapper.Map<SubjectViewModel, SubjectDTO>(subject);
                schoolService.Edit(_subject);
                return RedirectToAction("Index");
            }
            return View(subject);
        }
        [Authorize(Users = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sublectDtos = schoolService.GetSubject(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subject = mapper.Map<SubjectDTO, SubjectViewModel>(sublectDtos);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var subjectDtos = schoolService.GetSubject(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, SubjectViewModel>()).CreateMapper();
            var subject = mapper.Map<SubjectDTO, SubjectViewModel>(subjectDtos);

            schoolService.DeleteS(subject.Id);
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