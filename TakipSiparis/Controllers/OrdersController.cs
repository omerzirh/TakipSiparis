using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    public class OrdersController : Controller
    {
        SiparisTakipASPEntities db = new SiparisTakipASPEntities();



        public ActionResult Index()
        {
            var model = db.Orders.ToList();
            return View(model);
        }

        public ActionResult PendingApproval()
        {
            var model = db.Orders.Where(x => x.Decision == null).ToList();
            return View(model);
        }
        public ActionResult Add()
        {
            var model = new Orders();
            Reload(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Orders o)
        {
            if (!ModelState.IsValid)
            {
                var model = new Orders();
                Reload(model);
                return View(model);
            }
            if (Decimal.Compare(o.Price, 100)<0 || Decimal.Compare(o.Price, 100)==0)
            {
                o.Decision = "Accept";
            }
            
            o.UserName = User.Identity.GetUserName();

            db.Entry(o).State = System.Data.Entity.EntityState.Added;

            db.SaveChanges();

            return RedirectToAction("Approved");
        }
        //Listing dropdown elements
        private void Reload(Orders model)
        {
            List<Categories> categoryList = db.Categories.OrderBy(x => x.CategoryName).ToList();
            model.CategoryList = (from x in categoryList
                                  select new SelectListItem
                                  {
                                      Text = x.CategoryName,
                                      Value = x.CategoryName
                                  }).ToList();
            List<PaymentMethod> paymethList = db.PaymentMethod.OrderBy(x => x.PayMeth).ToList();
            model.PayMethList = (from x in paymethList
                                 select new SelectListItem
                                 {
                                     Text = x.PayMeth,
                                     Value = x.PayMeth
                                 }).ToList();
            List<Decision> decisionList = db.Decision.OrderBy(x => x.Decision1).ToList();
            model.DecisionList = (from x in decisionList
                                  select new SelectListItem
                                  {
                                      Text = x.Decision1,
                                      Value = x.Decision1
                                  }).ToList();
            List<Brands> brandList = db.Brands.OrderBy(x => x.BrandName).ToList();
            model.BrandList = (from x in brandList
                               select new SelectListItem
                               {
                                   Text = x.BrandName,
                                   Value = x.BrandName
                               }).ToList();
        

        }

        [HttpPost]
        [Authorize(Roles = "A")]
        public ActionResult ApproveResult(int id)
        {
            var ord = db.Orders.Find(id);
            ord.Decision = "Accept";
            db.Entry(ord).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Approved");
        }
        [HttpPost]
        public ActionResult DenyResult(int id)
        {
            string buttonClicked = Request.Form["btn"];
            var ord = db.Orders.Find(id);
            Console.WriteLine("buton" + buttonClicked);
            ord.Decision = "Decline";
            db.Entry(ord).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult OrderHistory()
        {

            var model = db.Orders.Where(x => x.Decision == "Accept" && x.TerminDate!=null && x.PaymentDate!=null).ToList();
            return View(model);

        }     
        public ActionResult OrderHistory2(string search)
        {
            
            var model = db.Orders.Where(x=>x.Decision=="Accept"&&x.TerminDate!=null).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.ProductName.Contains(search) || x.UserName.Contains(search)).ToList();

            }
            return View(model);

        }
        public ActionResult UpdateEditDelete()
        {

            var model = db.Orders.Where(x => x.Decision == null && x.TerminDate == null && x.PaymentDate == null).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult UpdateEdit(int id)
        {
            var model = db.Orders.Find(id);
            if (model == null) return HttpNotFound();
            return View(model);
        }      
        [HttpPost]
        public ActionResult UpdateEdit(Orders o, int id)
        {
            
        

            db.Entry(o).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UpdateEditDelete");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var model = db.Orders.FirstOrDefault(x => x.ID == id);
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("UpdateEditDelete");
        }
        
        public ActionResult Approved()
        {

            var model = db.Orders.Where(x => x.Decision == "Accept" && x.TerminDate==null&&x.PaymentDate==null).ToList();
            return View(model);

        }
        public ActionResult Declined()
        {

            var model = db.Orders.Where(x => x.Decision == "Decline" && x.TerminDate==null&&x.PaymentDate==null).ToList();
            return View(model);

        }
        public ActionResult PendingPayment()
        {

            var model = db.Orders.Where(x => x.Decision == "Accept" && x.PaymentDate==null).ToList();
            return View(model);

        }
        public ActionResult NewArrivals()
        {
            DateTime date_begin = DateTime.Today.AddDays(-7.0);
            DateTime date_end = DateTime.Today;
            var model = db.Orders.Where(x => x.Decision == "Accept" && x.TerminDate!=null).ToList();
            //var model = db.Orders.Where(x => x.DecisionID == 1 && x.TerminDate >= date_begin && x.TerminDate <= date_end).ToList();
            return View(model);

        }
     

        [HttpGet]
        public PartialViewResult Edit(Int32 id)
        {
            Orders ord = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            Orders ordN = new Orders();
            ordN.ID = Convert.ToInt32(ord.ID);
            ordN.Category = ord.Category;
            ordN.Brand = ord.Brand;
            ordN.ProductName = ord.ProductName;
            ordN.Amount = ord.Amount;
            ordN.Price = ord.Price;
            ordN.OrderDate = ord.OrderDate;
            ordN.PayMeth = ord.PayMeth;
            ordN.PaymentDate = ord.PaymentDate;
            ordN.AtchName = ord.AtchName;
            ordN.Note = ord.Note;
            ordN.UserName = ord.UserName;
            ordN.Decision = ord.Decision;
            return PartialView(ordN);

        }
        [HttpPost]
        public JsonResult Edit(Orders ord)
        { 
            Orders ordtb =db.Orders.Where(x => x.ID == ord.ID).FirstOrDefault();
            Orders ordNB = new Orders();
            ordtb.ID = Convert.ToInt32(ord.ID);
            ordtb.Category = ord.Category;
            ordtb.Brand = ord.Brand;
            ordtb.ProductName = ord.ProductName;
            ordtb.Amount = ord.Amount;
            ordtb.Price = ord.Price;
            ordtb.OrderDate = ord.OrderDate;
            ordtb.PayMeth = ord.PayMeth;
            ordtb.PaymentDate = ord.PaymentDate;
            ordtb.TerminDate = ord.TerminDate;
            ordtb.AtchName = ord.AtchName;
            ordtb.Note = ord.Note;
            ordtb.UserName = ord.UserName;
            ordtb.Decision = ord.Decision;
            db.SaveChanges();
            return Json(ordtb, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult AddPayment(Int32 id)
        {
            Orders ord = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            Orders ordN = new Orders();
            ordN.ID = Convert.ToInt32(ord.ID);
          
            ordN.PaymentDate = ord.PaymentDate;
           
            return PartialView(ordN);

        }
        [HttpPost]
        public JsonResult AddPayment(Orders ord)
        {
            Orders ordtb = db.Orders.Where(x => x.ID == ord.ID).FirstOrDefault();
            Orders ordNB = new Orders();
            ordtb.ID = Convert.ToInt32(ord.ID);
           
            ordtb.PaymentDate = ord.PaymentDate;
            
            db.SaveChanges();
            return Json(ordtb, JsonRequestBehavior.AllowGet);
        }

    }
}

/* var Id = Convert.ToInt32(id);
            var data = db.Orders.FirstOrDefault(x => x.ID == Id);
            return Json(

                new
                {
                    Result = new
                    {
                        Id,
                        data.CategoryID,
                        data.BrandID,
                        data.ProductName,
                        data.Amount,
                        data.Price,
                        data.OrderDate,
                        data.PayMethID,
                        data.PaymentDate,
                        data.AtchID,
                        data.TerminDate,
                        data.Note,
                        data.DecisionID,
                    }
                }, JsonRequestBehavior.AllowGet
                );*/
//[HttpPost]
//public JsonResult Refresh()
//{
//    var datas = db.Orders.ToList();
//    return Json(
//        new
//        {
//            Result = from obj in datas
//                     select new
//                     {
//                         obj.ID,
//                         obj.CategoryID,
//                         obj.BrandID,
//                         obj.ProductName,
//                         obj.Amount,
//                         obj.Price,
//                         obj.OrderDate,
//                         obj.PayMethID,
//                         obj.PaymentDate,
//                         obj.AtchID,
//                         obj.TerminDate,
//                         obj.Note,
//                         obj.DecisionID,
//                     }
//        }, JsonRequestBehavior.AllowGet);
//}
