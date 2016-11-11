using JQueryPopupModal.Entities;
using JQueryPopupModal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryPopupModal.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeServices eService = new EmployeeServices();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchEmployee(int page = 1, string sort = "name", string sortDir = "ASC")
        {
            const int pageSize = 5;
            var totalRow = eService.CountAllEmployee();

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "id", "name", "department", "country" };
            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "id";

            var employee = eService.GetEmployeePage(page, pageSize, "it." + sort + " " + sortDir);

            var data = new PagedEmployeeModel()
            {
                TotalRows = totalRow,
                PageSize = pageSize,
                Employee = employee
            };
            return View(data);
        }

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.IsUpdate = false;
                return View("_CrateEmployee");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateEmployee(EmployeeModel emp, string Command)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateEmployee", emp);
            }
            else
            {
                Employee employee = new Entities.Employee();
                employee.Emp_Id = emp.Emp_Id;
                employee.Name = emp.Name;
                employee.Department = emp.Department;
                employee.City = emp.City;
                employee.State = emp.State;
                employee.Country = emp.Country;
                employee.Mobile = emp.Mobile;

                bool IsSuccess = eService.AddEmployee(employee);
                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Employee Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchEmployee", "Employee");
                }
            }
            return PartialView("_CreateEmployee");
        }

        public ActionResult EditEmployee(int id)
        {
            var data = eService.GetEmployeeDetail(id);

            if (Request.IsAjaxRequest())
            {
                EmployeeModel employeeModel = new EmployeeModel();

                employeeModel.Emp_Id = data.Emp_Id;
                employeeModel.Name = data.Name;
                employeeModel.Department = data.Department;
                employeeModel.City = data.City;
                employeeModel.State = data.State;
                employeeModel.Country = data.Country;
                employeeModel.Mobile = data.Mobile;

                ViewBag.IsUpdate = true;
                return View("_EditEmployee", employeeModel);
            }
            else
            {
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeModel emp, string command)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditEmployee", emp);
            }
            else
            {
                Employee employee = new Entities.Employee();
                employee.Id = emp.Id;
                employee.Emp_Id = emp.Emp_Id;
                employee.Name = emp.Name;
                employee.Department = emp.Department;
                employee.City = emp.City;
                employee.State = emp.State;
                employee.Country = emp.Country;
                employee.Mobile = emp.Mobile;

                bool IsSuccess = eService.UpdateEmployee(employee);
                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Employee Updated Successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchEmployee", "Employee");
                }
            }
            return PartialView("_EditEmployee");
        }

        public ActionResult ViewEmployeeDetail(int id)
        {
            var data = eService.GetEmployeeDetail(id);
            if (Request.IsAjaxRequest())
            {
                EmployeeModel employeeModel = new EmployeeModel();

                employeeModel.Emp_Id = data.Emp_Id;
                employeeModel.Name = data.Name;
                employeeModel.Department = data.Department;
                employeeModel.City = data.City;
                employeeModel.State = data.State;
                employeeModel.Country = data.Country;
                employeeModel.Mobile = data.Mobile;

                return View("_EmployeeDetail", employeeModel);
            }
            else
            {
                return View(data);
            }
        }

        public ActionResult Delete(int id)
        {
            bool check = eService.DeleteEmployee(id);
            var data = eService.GetEmployees();
            return RedirectToAction("SearchEmployee");
        }
    }
}