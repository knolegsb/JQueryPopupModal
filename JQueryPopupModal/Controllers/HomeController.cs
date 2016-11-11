//using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQueryPopupModal;
using JQueryPopupModal.Entities;

namespace JQueryPopupModal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetContacts()
        {
            List<Contact> allContacts = null;
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                var contacts = (from a in db.Contacts
                                join b in db.Countries on a.CountryId equals b.CountryId
                                join c in db.States on a.StateId equals c.StateId
                                select new
                                {
                                    a,
                                    b.CountryName,
                                    c.StateName
                                });
                if (contacts != null)
                {
                    allContacts = new List<Contact>();
                    foreach (var i in contacts)
                    {
                        Contact con = i.a;
                        con.CountryName = i.CountryName;
                        con.StateName = i.StateName;
                        allContacts.Add(con);
                    }
                }
            }
            //return new JsonResult { Data = allContacts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return Json(allContacts, JsonRequestBehavior.AllowGet);
        }

        // Fetch Country from database
        private List<Country> GetCountry()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Countries.OrderBy(a => a.CountryName).ToList();
            }
        }

        // Fetch State from database
        private List<State> GetState(int countryId)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.States.Where(a => a.CountryId.Equals(countryId)).OrderBy(a => a.StateName).ToList();
            }
        }

        // return states as json data
        public JsonResult GetStateList(int countryId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return new JsonResult { Data = GetState(countryId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public Contact GetContact(int contactId)
        {
            Contact contact = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var v = (from a in db.Contacts
                         join b in db.Countries on a.CountryId equals b.CountryId
                         join c in db.States on a.StateId equals c.StateId
                         where a.ContactId.Equals(contactId)
                         select new
                         {
                             a,
                             b.CountryName,
                             c.StateName
                         }).FirstOrDefault();
                if (v != null)
                {
                    contact = v.a;
                    contact.CountryName = v.CountryName;
                    contact.StateName = v.StateName;
                }
                return contact;
            }
        }

        public ActionResult Save(int id = 0)
        {
            List<Country> Country = GetCountry();
            List<State> States = new List<State>();

            if (id > 0)
            {
                var c = GetContact(id);
                if (c != null)
                {
                    ViewBag.Countries = new SelectList(Country, "CountryId", "CountryName", c.CountryId);
                    ViewBag.States = new SelectList(GetState(c.CountryId), "StateId", "StateName", c.StateId);
                }
                else
                {
                    return HttpNotFound();
                }
                return PartialView("Save", c);
            }
            else
            {
                ViewBag.Countries = new SelectList(Country, "CountryId", "CountryName");
                ViewBag.States = new SelectList(States, "StateId", "StateName");
                return PartialView("Save");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Contact con)
        {
            string message = "";
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    if (con.ContactId > 0)
                    {
                        var v = db.Contacts.Where(a => a.ContactId.Equals(con.ContactId)).FirstOrDefault();
                        if (v != null)
                        {
                            v.ContactPerson = con.ContactPerson;
                            v.ContactNo = con.ContactNo;
                            v.CountryId = con.CountryId;
                            v.StateId = con.StateId;
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                    else
                    {
                        db.Contacts.Add(con);
                    }
                    db.SaveChanges();
                    status = true;
                    message = "Successfully Saved.";
                }
            }
            else
            {
                message = "Error! Please try again.";
            }

            return new JsonResult { Data = new { status = status, message = message } };
        }

        public ActionResult Delete(int id)
        {
            var con = GetContact(id);
            if (con == null)
            {
                return HttpNotFound();
            }
            return PartialView(con);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteContact(int id)
        {
            bool status = false;
            string message = "";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var v = db.Contacts.Where(a => a.ContactId.Equals(id)).FirstOrDefault();
                if (v != null)
                {
                    db.Contacts.Remove(v);
                    db.SaveChanges();
                    status = true;
                    message = "Successfully Deleted!";
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return new JsonResult { Data = new { status = status, message = message } }; 
        }
    }
}