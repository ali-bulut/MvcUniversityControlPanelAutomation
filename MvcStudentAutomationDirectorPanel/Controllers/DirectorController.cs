using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Models;
using EntityLayer.Models.ViewModel;
using MailSendLayer;
namespace MvcStudentAutomationDirectorPanel.Controllers
{
    [UserAuthorize]
    public class DirectorController : Controller
    {
        StudentAutomationContext db = new StudentAutomationContext();
        // GET: Director
        public ActionResult Index()
        {
            int DirectorID = Convert.ToInt32(Session["DirectorID"]);
            TeacherLessonViewModel tl = new TeacherLessonViewModel();

            tl.Department = db.Departments.ToList();
            tl.Lesson = db.Lessons.ToList();
            tl.Student = db.Students.ToList();
            tl.Teacher = db.Teachers.ToList();
            tl.StudentRegistration = db.StudentRegistrations.ToList();
            tl.Director = db.Directors.Where(x => x.DirectorID == DirectorID).ToList();
            tl.TeacherRegistration = db.TeacherRegistrations.ToList();
            return View(tl);
        }
        public ActionResult TeacherRegistration()
        {
            int DirectorID = Convert.ToInt32(Session["DirectorID"]);
            TeacherLessonViewModel tl = new TeacherLessonViewModel();
            tl.TeacherRegistration = db.TeacherRegistrations.ToList();
            tl.Department = db.Departments.ToList();
            tl.Director = db.Directors.Where(x => x.DirectorID == DirectorID).ToList();
            return View(tl);
        }

        [HttpPost]
        public JsonResult ConfirmTeacher(int TeacherID)
        {
            var teacher = db.TeacherRegistrations.Where(x => x.TeacherRegisterNo == TeacherID).SingleOrDefault();

            Teacher t = new Teacher();
            t.BirthDate = teacher.BirthDate;
            t.BirthPlace = teacher.BirthPlace;
            t.DepartmentID = teacher.DepartmentID;
            t.Email = teacher.Email;
            t.Image = teacher.Image;
            t.Name = teacher.Name;
            t.Password = teacher.Password;
            t.RegistrationDate = DateTime.Now;
            t.Surname = teacher.Surname;
            t.TrIdNo = teacher.TrIdNo;
            teacher.Confirmation = true;
            db.Teachers.Add(t);

            Mail mail = new Mail();
            string body = string.Format("Sevgili {0} {1}, Okulumuza {2} Tarihinde Yapmış Olduğunuz Başvuru Kabul Edilmiştir. Okulumuza Gelmeniz Rica Olunur.", t.Name, t.Surname, teacher.ApplicationDate);
            mail.MailSender(body, t.Email, "Öğretmen Başvurusu");

            db.TeacherRegistrations.Remove(teacher);
            db.SaveChanges();

            return Json(new
            {
                isRedirect = true,
                redirectUrl = Url.Action("TeacherRegistration"),
                message = "Öğretmen Başvurusu Başarıyla Kabul Edildi."
            });
        }

        [HttpPost]
        public JsonResult RefusalTeacher(int TeacherID)
        {
            var teacher = db.TeacherRegistrations.Where(x => x.TeacherRegisterNo == TeacherID).SingleOrDefault();

            teacher.Confirmation = false;
            Mail mail = new Mail();
            string body = string.Format("Sevgili {0} {1}, Okulumuza {2} Tarihinde Yapmış Olduğunuz Başvuru Reddedilmiştir.", teacher.Name, teacher.Surname, teacher.ApplicationDate);
            mail.MailSender(body, teacher.Email, "Öğretmen Başvurusu");

            db.TeacherRegistrations.Remove(teacher);
            db.SaveChanges();

            return Json(new
            {
                isRedirect = true,
                redirectUrl = Url.Action("TeacherRegistration"),
                message = "Öğretmen Başvurusu Reddedildi."
            });
        }

        public ActionResult StudentRegistr()
        {
            int DirectorID = Convert.ToInt32(Session["DirectorID"]);
            TeacherLessonViewModel tl = new TeacherLessonViewModel();
            tl.StudentRegistration = db.StudentRegistrations.ToList();
            tl.Department = db.Departments.ToList();
            tl.Teacher = db.Teachers.ToList();
            tl.Director = db.Directors.Where(x => x.DirectorID == DirectorID).ToList();
            return View(tl);
        }

        [HttpPost]
        public JsonResult ConfirmStudent(int StudentID, int onayciBir, int onayciIki)
        {
            var student = db.StudentRegistrations.Where(x => x.StudentRegisterNo == StudentID).SingleOrDefault();

            if (string.IsNullOrEmpty(onayciBir.ToString()) == false)
            {
                StudentConfirmation confirm = new StudentConfirmation();
                confirm.StudentRegisterNo = StudentID;
                confirm.TeacherID = onayciBir;
                confirm.Row = 1;
                db.StudentConfirmations.Add(confirm);
                db.SaveChanges();
            }
            if (string.IsNullOrEmpty(onayciIki.ToString()) == false)
            {
                StudentConfirmation confirm = new StudentConfirmation();
                confirm.StudentRegisterNo = StudentID;
                confirm.TeacherID = onayciIki;
                confirm.Row = 2;
                db.StudentConfirmations.Add(confirm);
                db.SaveChanges();
            }

            student.Status = false; //öğrenciyi öğretmenlere attı.
            db.SaveChanges();

            return Json(new
            {
                isRedirect = true,
                redirectUrl = Url.Action("StudentRegistration"),
                message = "Öğrenci Başvurusu Öğretmenlere İletildi."
            });
        }

        [HttpPost]
        public JsonResult RefusalStudent(int StudentID)
        {
            var student = db.StudentRegistrations.Where(x => x.StudentRegisterNo == StudentID).SingleOrDefault();

            Mail mail = new Mail();
            string body = string.Format("Sevgili {0} {1}, Okulumuza {2} Tarihinde Yapmış Olduğunuz Başvuru Reddedilmiştir.", student.Name, student.Surname, string.Format("{0:D}", student.ApplicationDate));
            mail.MailSender(body, student.Email, "Öğrenci Başvurusu");

            db.StudentRegistrations.Remove(student);
            db.SaveChanges();

            return Json(new
            {
                isRedirect = true,
                redirectUrl = Url.Action("StudentRegistration"),
                message = "Öğrenci Başvurusu Reddedildi."
            });
        }

        [HttpPost]
        public JsonResult Revize(int studentID, string msg)
        {
            StudentRegistration register = db.StudentRegistrations.Where(x => x.StudentRegisterNo == studentID).SingleOrDefault();

            Mail mail = new Mail();

            string body = string.Format("Sevgili Öğrenci Adayımız Yöneticimizin Size Bir Revize Talebi Var. Lütfen Mesajı Dikkate Alınız. <br> Mesaj: {0}", msg);
            mail.MailSender(body, register.Email, "Revize Talebi");

            return Json(new
            {
                isRedirect = true,
                redirectUrl = Url.Action("StudentRegistr"),
                message = "Öğrenci Adayına Mail Gönderildi."
            });
        }

        public ActionResult AddLessonAndDepartment()
        {
            int DirectorID=Convert.ToInt32(Session["DirectorID"]);
            TeacherLessonViewModel tl = new TeacherLessonViewModel();
            tl.Lesson = db.Lessons.ToList();
            tl.Department = db.Departments.ToList();
            tl.Director = db.Directors.Where(x => x.DirectorID == DirectorID).ToList();

            return View(tl);
        }

        [HttpPost]
        public ActionResult AddDepartment(string DepartmentName)
        {
            Department department = new Department();
            department.DepartmentName = DepartmentName;
            db.Departments.Add(department);
            db.SaveChanges();
            return RedirectToAction("AddLessonAndDepartment");
        }

        [HttpPost]
        public JsonResult AddLesson(string LessonName, int DepartmentID)
        {
            Lesson lesson = new Lesson();
            lesson.LessonName = LessonName;
            lesson.DepartmentID = DepartmentID;
            db.Lessons.Add(lesson);
            db.SaveChanges();

            return Json(new
            {
                isRedirect = true,
                redirectUrl = Url.Action("AddLessonAndDepartment"),
                message= "Ders Başarıyla Eklendi."
            });
        }

        public ActionResult Errorpage()
        {
            return View();
        }
    }
}