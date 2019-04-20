using AutoMapper;
using PagedList;
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
    public class PupilController : Controller
    {
        ISchoolService schoolService;

        public PupilController(ISchoolService school)
        {
            schoolService = school;
        }

        // GET: Pupils
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            IEnumerable<PupilDTO> pupilDtos = schoolService.GetPupils();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
            var pupils = mapper.Map<IEnumerable<PupilDTO>, List<PupilViewModel>>(pupilDtos);


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pupls = from s in pupils
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pupls = pupls.Where(s => s.LastName.Contains(searchString.ToUpper())
                                       || s.LastName.Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pupls = pupls.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    pupls = pupls.OrderBy(s => s.Sex);
                    break;
                case "date_desc":
                    pupls = pupls.OrderByDescending(s => s.Sex);
                    break;
                default:  // Name ascending 
                    pupls = pupls.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(pupls.ToPagedList(pageNumber, pageSize));
        }

        // GET: Pupils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pupilDtos = schoolService.GetPupil(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
            var pupil = mapper.Map<PupilDTO, PupilViewModel>(pupilDtos);

            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            ViewBag.selectedClass = new SelectList(schoolService.GetClases(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstMidlName,Sex,Age,Birthday, Classs_Id")] PupilViewModel pupil, int? selectedClass)
        {
            TimeSpan age;

            age = DateTime.Now - pupil.Birthday;
            if (ModelState.IsValid)
            {
                pupil.ClassId = selectedClass;
                //if (selectedClass != 0)
                //{
                //    var item = schoolService.GetClass(selectedClass);

                //    var mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<ClassDTO, ClassViewModel>()).CreateMapper();
                //    var @class = mapper1.Map<ClassDTO, ClassViewModel>(item);
                //    pupil.Class = @class;
                //}

                pupil.Age = age.Days / 365;

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilViewModel, PupilDTO>()).CreateMapper();
                var _pupil = mapper.Map<PupilViewModel, PupilDTO>(pupil);

                schoolService.Create(_pupil);
                return RedirectToAction("Index");
            }

            return View(pupil);
        }
        [Authorize(Users = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pupilDtos = schoolService.GetPupil(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
            var pupil = mapper.Map<PupilDTO, PupilViewModel>(pupilDtos);
            ViewBag.selectedClass = new SelectList(schoolService.GetClases(), "Id", "Name");

            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }

        // POST: Pupils/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstMidlName,Sex,Age,Birthday")] PupilViewModel pupil, int? selectedClass)
        {
            if (ModelState.IsValid)
            {
                TimeSpan time;
                time = DateTime.Now - pupil.Birthday;

                pupil.Age = time.Days / 365;
                pupil.ClassId = selectedClass;

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilViewModel, PupilDTO>()).CreateMapper();
                var _pupil = mapper.Map<PupilViewModel, PupilDTO>(pupil);
                schoolService.Edit(_pupil);

                return RedirectToAction("Index");
            }
            return View(pupil);
        }
        [Authorize(Users = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pupilDtos = schoolService.GetPupil(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
            var pupil = mapper.Map<PupilDTO, PupilViewModel>(pupilDtos);

            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }

        // POST: Pupils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pupilDtos = schoolService.GetPupil(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PupilDTO, PupilViewModel>()).CreateMapper();
            var pupil = mapper.Map<PupilDTO, PupilViewModel>(pupilDtos);

            schoolService.DeleteP(pupil.Id);
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