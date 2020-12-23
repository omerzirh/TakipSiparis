using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    public class PaymethodsController : Controller
    {
        SiparisTakipASPEntities db = new SiparisTakipASPEntities();
        public ActionResult Index()
        {
            var model = db.PaymentMethod.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(PaymentMethod p)
        {
            if (!ModelState.IsValid) return View("Add");
            db.PaymentMethod.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}