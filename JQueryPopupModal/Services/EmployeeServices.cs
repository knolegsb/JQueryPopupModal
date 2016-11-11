using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JQueryPopupModal.Services
{
    public class EmployeeServices
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        public IEnumerable<Employee> GetEmployeePage(int pageNumber, int pageSize, string searchCriteria)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            return db.Employees.OrderBy(m => m.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountAllEmployee()
        {
            return db.Employees.Count();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Employee GetEmployeeDetail(int mCustId)
        {
            return db.Employees.Where(m => m.Id == mCustId).FirstOrDefault();
        }

        public bool AddEmployee(Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                Employee employee = db.Employees.Where(m => m.Id == emp.Id).FirstOrDefault();

                employee.Name = emp.Name;
                employee.Department = emp.Department;
                employee.City = emp.City;
                employee.State = emp.State;
                employee.Country = emp.Country;
                employee.Mobile = emp.Mobile;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEmployee(int custId)
        {
            try
            {
                Employee employee = db.Employees.Where(m => m.Id == custId).FirstOrDefault();
                db.Employees.Remove(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class PagedEmployeeModel
    {
        public int TotalRows { get; set; }
        public IEnumerable<Employee> Employee { get; set; }
        public int PageSize { get; set; }
    }

    public class EmployeeModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Employee Code")]
        public string Emp_Id { get; set; }

        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Enter Department")]
        public string Department { get; set; }

        [Required(ErrorMessage ="Please Enter City")]
        public string City { get; set; }

        [Required(ErrorMessage ="Please Enter State")]
        public string State { get; set; }

        [Required(ErrorMessage ="Please Enter Country")]
        public string Country { get; set; }

        [Required(ErrorMessage ="Please Enter Mobile")]
        public string Mobile { get; set; }
    }
}