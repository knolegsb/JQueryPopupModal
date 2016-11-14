using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryPopupModal.Controllers
{
    public class AjaxEmployeeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: AjaxEmployee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            var employees = _context.Employees.ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployee(int id)
        {
            var employee = _context.Employees.ToList().Find(m => m.Id == id);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
            }
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return Json(employee, JsonRequestBehavior.AllowGet);
        }
    }
}