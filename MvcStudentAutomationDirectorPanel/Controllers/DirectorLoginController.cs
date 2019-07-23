using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Models;
using System.Web.Helpers;

namespace MvcStudentAutomationDirectorPanel.Controllers
{
    public class DirectorLoginController : Controller
    {
        StudentAutomationContext db = new StudentAutomationContext();  
        // GET: DirectorLogin
        public ActionResult SignIn()
        {
            if (Session["DirectorID"]!=null)
            {
                Session["DirectorID"] = null;
            }
            var directorList = db.Directors.ToList();
            var errorMessage = TempData["ErrorMessage"];
            return View(directorList);
        }

        [HttpPost]
        public ActionResult SignIn(string Email, string Password)
        {
            string pass = Crypto.Hash(Password, "MD5");
            var isTrue = db.Directors.Where(x => x.Email == Email && x.Password == pass).SingleOrDefault();

            if (isTrue != null)
            {
                Session["DirectorID"] = isTrue.DirectorID.ToString();
                HttpCookie cookie = new HttpCookie("Name", isTrue.Name + " " + isTrue.Surname);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Director");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı Email veya Şifre Girdiniz.";
                return RedirectToAction("SignIn");
            }
        }
    }
}