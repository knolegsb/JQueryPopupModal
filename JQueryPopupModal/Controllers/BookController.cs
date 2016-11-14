using JQueryPopupModal.Entities;
using JQueryPopupModal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryPopupModal.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext _context;

        public BookController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBooks()
        {
            var books = _context.Books.ToList();

            return Json(books, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var book = _context.Books.ToList().Find(m => m.Id == id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = _context.Books.ToList().Find(m => m.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}