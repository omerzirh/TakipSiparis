using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    [Authorize(Roles ="A")]
    public class DecisionsController : Controller
    {
        SiparisTakipASPEntities db = new SiparisTakipASPEntities();
        public ActionResult Index()
        {
            var model = db.Decision.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Decision d)
        {
            if (!ModelState.IsValid) return View("Add");
            db.Decision.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}