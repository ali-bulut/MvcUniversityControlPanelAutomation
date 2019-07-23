using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Models;
using EntityLayer.Models.ViewModel;
using MailSendLayer;
namespace MvcStudentAutomationStudentPanel.Controllers
{
    [UserAuthorize]
    public class StudentController : Controller
    {
        StudentAutomationContext db = new StudentAutomationContext();
        // GET: Student
        public ActionResult Index()
        {
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);
            TeacherLessonViewModel t = new TeacherLessonViewModel();
            t.Lesson = db.Lessons.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.Student = db.Students.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.Teacher = db.Teachers.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.StudentLesson = db.StudentLessons.Where(x => x.StudentNumber == StudentID).ToList();
            t.Department = db.Departments.ToList();
            return View(t);
        }

        public ActionResult ExamNote()
        {
            int StudentID=Convert.ToInt32(Session["StudentID"]);
            int DepartmentID=Convert.ToInt32(Session["DepartmentID"]);
            TeacherLessonViewModel tl = new TeacherLessonViewModel();
            tl.Student = db.Students.Where(x => x.StudentNumber == StudentID).ToList();
            tl.StudentLesson = db.StudentLessons.Where(x => x.StudentNumber == StudentID).ToList();
            tl.Lesson = db.Lessons.Where(x => x.DepartmentID == DepartmentID).ToList();
            return View(tl);
        }

        [HttpPost]
        public JsonResult BringExamNote(int LessonID)
        {
          int StudentID = Convert.ToInt32(Session["StudentID"]);

          List<StudentLesson> student = db.StudentLessons.Where(x => x.StudentNumber == StudentID && x.LessonID == LessonID).ToList();
          return Json(student);
        }

        public ActionResult LessonSelection()
        {
            TeacherLessonViewModel tl = new TeacherLessonViewModel();
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);
            tl.Student = db.Students.Where(x => x.StudentNumber == StudentID).ToList();
            tl.Lesson = db.Lessons.Where(x => x.DepartmentID == DepartmentID).ToList();
            tl.StudentLesson = db.StudentLessons.Where(x => x.StudentNumber == StudentID).ToList();
            tl.Teacher = db.Teachers.Where(x => x.DepartmentID == DepartmentID).ToList();
            tl.TeacherLesson = db.TeacherLessons.ToList();
            return View(tl);
        }

        [HttpPost]
        public JsonResult AddLesson(int LessonID,int TeacherID)
        {
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            StudentLesson sl = new StudentLesson();

            var lesson = db.StudentLessons.Where(x => x.StudentNumber == StudentID &&  x.LessonID == LessonID).SingleOrDefault();

            if (lesson == null)
            {
                sl.LessonID = LessonID;
                sl.StudentNumber = StudentID;
                sl.TeacherID = TeacherID;

                db.StudentLessons.Add(sl);
                db.SaveChanges();

                return Json(new
                {
                    redirectUrl = Url.Action("LessonSelection"),
                    isRedirect = true,
                    message = "Dersiniz Başarıyla Eklendi"
                });
            }
            else
            {
                return Json(new
                {
                    isRedirect = false,
                    message = "Bu Dersi Zaten Daha Önce Seçmişsiniz"
                });
            }
        }


        public ActionResult Settings()
        {
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            var student = db.Students.Where(x => x.StudentNumber == StudentID).SingleOrDefault();

            return View(student);
        }

        [HttpPost]
        public ActionResult Settings(string Name, string Surname, string BirthPlace, string Email, string Password)
        {
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            Student student = db.Students.Where(x => x.StudentNumber == StudentID).SingleOrDefault();

            if (Request.Files.Count > 0)
            {
                System.IO.File.Delete(Server.MapPath("~/StudentImages/" + student.Image));

                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                string extension = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string filePath = "~/StudentImages/" + fileName + extension;

                Request.Files[0].SaveAs(Server.MapPath(filePath));

                student.Image = fileName + extension;
            }

            student.Name = Name;
            student.Surname = Surname;
            student.BirthPlace = BirthPlace;
            student.Email = Email;
            student.Password = Password;

            db.SaveChanges();

            TempData["Message"] = "Bilgileriniz Başarıyla Düzenlendi";
            return RedirectToAction("Index");
        }
        public ActionResult Errorpage()
        {
            return View();
        }
    }
}