using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    public class CategoriesController : Controller
    {
        SiparisTakipASPEntities db = new SiparisTakipASPEntities();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Categories c)
        {
            if (!ModelState.IsValid) return View("Add");
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}