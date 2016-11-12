using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using JQueryPopupModal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace JQueryPopupModal.Controllers
{
    public class PhoneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Phone
        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "PhoneId", string sortdir = "ASC")
        {
            var records = new PagedList<Phone>();
            ViewBag.filter = filter;
            records.Content = db.Phones
                                .Where(p => filter == null
                                        || (p.Model.Contains(filter))
                                        || p.Company.Contains(filter))
                                .OrderBy(sort + " " + sortdir)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            records.TotalRecords = db.Phones
                                    .Where(p => filter == null
                                              || (p.Model.Contains(filter))
                                              || p.Company.Contains(filter))
                                    .Count();
            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);
        }

        // GET: /Phone/Details/5
        public ActionResult Details(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("Partials/Details", phone);
        }

        // GET: /Phone/Create
        [HttpGet]
        public ActionResult Create()
        {
            var phone = new Phone();
            return PartialView("Partials/Create", phone);
        }

        // POST: /Phone/Create
        [HttpPost]
        public JsonResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(phone, JsonRequestBehavior.AllowGet);
        }

        // GET: /Phone/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return PartialView("Partials/Edit", phone);
        }

        // POST: /Phone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Partials/Edit", phone);
        }

        // GET: /Phone/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("Partials/Delete", phone);
        }

        // POST: /Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}