using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Models;
using System.Web.Helpers;
namespace PasswordHashing.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        StudentAutomationContext db = new StudentAutomationContext();
        public ActionResult Index()
        {
            return View(db.StudentRegistrations.ToList()); 
        }

        [HttpPost]
        public ActionResult Index(string Password)
        {
            StudentRegistration sr = new StudentRegistration();
            string hash = Crypto.HashPassword(Password);
            sr.Password = "13424";
            sr.Name = "Ali";
            db.StudentRegistrations.Add(sr);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}