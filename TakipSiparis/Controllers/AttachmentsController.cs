using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipSiparis.Models;

namespace TakipSiparis.Controllers
{
    public class AttachmentsController : Controller
    {
        // GET: Attachments
        SiparisTakipASPEntities db = new SiparisTakipASPEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAttachment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAttachment(Attachments atch, HttpPostedFileBase file)
        {
            Attachments files = new Attachments();
            string path = UploadFile(file);
            if (path.Equals("-1"))
            {

            }
            else
            {
                atch.FileName = Path.GetFileName(file.FileName);
                atch.FileContent = path;
                atch.FileExtension = Path.GetExtension(file.FileName);
                db.Attachments.Add(atch);
                db.SaveChanges();
                ViewBag.msg = "data added";
            }
            return View();
        }

        private string UploadFile(HttpPostedFileBase file)
        {
            string path = "-1";
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    path = Path.Combine(Server.MapPath("~/Attachments"), DateTime.Now.ToString("yyyyMMdd") + Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    path = "~/Attachments/" + DateTime.Now.ToString("yyyyMMdd") + Path.GetFileName(file.FileName);
                }
                catch (Exception ex)
                {
                    ViewBag.file_error = ex.Message;
                    path = "-1";
                }
                return path;
            }
            else
            {
                Response.Write("<script>alert('Select a file');</script>");
                path = "-1";
                return path;
            }
        }
    }
}