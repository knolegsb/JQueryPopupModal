using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryPopupModal.Controllers
{
    public class EmployeeModalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmployeeModal
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        [HttpGet]
        public ActionResult AddEditRecord(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if(id != null)
                {
                    ViewBag.IsUpdate = true;
                    Employee employee = db.Employees.Where(m => m.Id == id).FirstOrDefault();
                    return PartialView("_EmployeeList", employee);
                }
                ViewBag.IsUpdate = false;
                return PartialView("_EmployeeList");
            }
            else
            {
                if (id != null)
                {
                    ViewBag.IsUpdate = true;
                    Employee employee = db.Employees.Where(m => m.Id == id).FirstOrDefault();
                    return PartialView("EmployeeList", employee);
                }
                ViewBag.IsUpdate = false;
                return View("EmployeeList");
            }
        }

        [HttpPost]
        public ActionResult AddEditRecord(Employee employee, string cmd)
        {
            if (ModelState.IsValid)
            {
                if (cmd == "Save")
                {
                    try
                    {
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        Employee emp = db.Employees.Where(m => m.Id == employee.Id).FirstOrDefault();
                        if (emp != null)
                        {
                            emp.Emp_Id = employee.Emp_Id;
                            emp.Name = employee.Name;
                            emp.Department = employee.Department;
                            emp.City = employee.City;
                            emp.State = employee.State;
                            emp.Country = employee.Country;
                            emp.Mobile = employee.Mobile;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    catch { }
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EmployeeList", employee);
            }
            else
            {
                return View("EmployeeList", employee);
            }
        }

        public ActionResult DeleteRecord(int id)
        {
            Employee employee = db.Employees.Where(m => m.Id == id).FirstOrDefault();
            if (employee != null)
            {
                try
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
                catch { }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Employee employee = db.Employees.Where(m => m.Id == id).FirstOrDefault();
            if (employee != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_EmployeeDetails", employee);
                }
                else
                {
                    return View("EmployeeDetails", employee);
                }
            }
            return View("Index");
        }
    }
}