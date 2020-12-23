using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        SiparisTakipASPEntities db = new SiparisTakipASPEntities();
        public ActionResult Index()
        {
            var model = db.Brands.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Brands b)
        {
            if (!ModelState.IsValid) return View("Add");
            db.Brands.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}