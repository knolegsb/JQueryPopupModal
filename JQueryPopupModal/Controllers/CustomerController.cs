using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryPopupModal.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDB _dbContext = new CustomerDB();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(_dbContext.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Customer cus)
        {
            return Json(_dbContext.Add(cus), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetById(int id)
        {
            var customer = _dbContext.ListAll().Find(x => x.CustomerId.Equals(id));
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Customer cus)
        {
            return Json(_dbContext.Update(cus), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            return Json(_dbContext.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}