using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {

        SiparisTakipASPEntities db = new SiparisTakipASPEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            var model = db.Users.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(Users u)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == u.Username && x.Password == u.Password);
            if (user != null)
            {
                FormsAuthentication.RedirectFromLoginPage(u.Username, false);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.error = "username or password is wrong";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }

        [Authorize(Roles = "A,D")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize(Roles ="A,D")]
        [HttpPost]
        public ActionResult Add(Users u)
        {
            if (!ModelState.IsValid) return View("Add");
            int MAX = db.Users.Max(x => x.ID);
           

            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}