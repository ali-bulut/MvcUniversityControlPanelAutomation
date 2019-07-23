using EntityLayer.Models;
using EntityLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcStudentAutomation.Controllers
{
    public class TipsController : Controller
    {
        // GET: Tips
        StudentAutomationContext db = new StudentAutomationContext();
        public ActionResult Charts()
        {
            return View();
        }

        public ActionResult Icons()
        {
            return View();
        }

        public ActionResult Typography()
        {
            return View();
        }
        [UserAuthorize]
        public ActionResult LessonSelection()
        {
            var model = new TeacherLessonViewModel();

            int ID = Convert.ToInt32(Session["ID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);
            model.Teacher = db.Teachers.Where(x => x.TeacherID == ID).ToList();
            model.Lesson = db.Lessons.Where(x => x.DepartmentID == DepartmentID).ToList();
            model.TeacherLesson = db.TeacherLessons.ToList();
            model.StudentLesson = db.StudentLessons.ToList();
            model.Student = db.Students.ToList();
            if (System.Web.HttpContext.Current.Request.Cookies["lang"] != null)
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["lang"];
                string lang = cookie.Value;
                var culture = new System.Globalization.CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            else
            {
                var culture = new System.Globalization.CultureInfo("tr-TR");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult AddLesson(int ID)
        {
            int TeacherID = Convert.ToInt32(Session["ID"]);
            TeacherLesson tl = new TeacherLesson();
            var a = db.TeacherLessons.Where(y => y.TeacherID == TeacherID).Where(x => x.LessonID == ID).SingleOrDefault();
            if (a == null)
            {
                tl.LessonID = ID;
                tl.TeacherID = TeacherID;
                db.TeacherLessons.Add(tl);
                db.SaveChanges();
                return Json("Ders Başarıyla Eklendi");
            }
            else
            {
                return Json("Bu Dersi Zaten Daha Önce Seçmişsiniz");
            }
        }
        public ActionResult Errorpage()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Buttonpage()
        {
            return View();
        }

        public ActionResult Tables()
        {
            return View();
        }

        public ActionResult Inputpage()
        {
            return View();
        }

        public ActionResult Validation()
        {
            return View();
        }

        public List<Teacher> List()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var list = db.Teachers.Where(x => x.TeacherID == ID).ToList();

            return list;
        }
    }
}