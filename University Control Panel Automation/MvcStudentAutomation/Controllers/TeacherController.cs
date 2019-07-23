using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Models;
using EntityLayer.Models.ViewModel;
using MailSendLayer;
using AutoMapper;
namespace MvcStudentAutomation.Controllers
{
    [UserAuthorize]
    public class TeacherController : Controller
    {
        //cookie oluşturma
        /*
           HttpCokie cookie=new HttpCokie("isim",deger);
           Response.Cookies.add(cookie);
         */

        //cookie yakalamak
        /*
         HttpCokie setcookie=Request.Cookies.Get("isim");
         string isim=setcookie.Value; 
         */
        StudentAutomationContext db = new StudentAutomationContext();
        public ActionResult Index()
        {
            //HttpCookie setCookie = Request.Cookies.Get("ID");
            //int ID = Convert.ToInt32(setCookie.Value);

            int ID = Convert.ToInt32(Session["ID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);
            TeacherLessonViewModel t = new TeacherLessonViewModel();
            t.Lesson = db.Lessons.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.Student = db.Students.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.Teacher = db.Teachers.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.TeacherLesson = db.TeacherLessons.Where(x => x.TeacherID == ID).ToList();
            t.StudentLesson = db.StudentLessons.Where(x => x.TeacherID == ID).ToList();
            t.Department = db.Departments.ToList();
            ChangeLanguage();
            return View(t);
        }

        public ActionResult ExamNote()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);
            TeacherLessonViewModel t = new TeacherLessonViewModel();
            t.Lesson = db.Lessons.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.Student = db.Students.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.Teacher = db.Teachers.Where(x => x.DepartmentID == DepartmentID).ToList();
            t.TeacherLesson = db.TeacherLessons.Where(x => x.TeacherID == ID).ToList();
            t.StudentLesson = db.StudentLessons.Where(x => x.TeacherID == ID).ToList();
            t.Department = db.Departments.ToList();
            ChangeLanguage();
            return View(t);
        }

        [HttpPost]
        public ActionResult ExamNote(StudentLesson sl)
        {
            int? Exam1 = 0;
            int? Exam2 = 0;
            Exam1 = Convert.ToInt32(sl.Exam1);
            Exam2 = Convert.ToInt32(sl.Exam2);
            if (Exam1 == 0)
            {
                Exam1 = null;
            }
            if (Exam2 == 0)
            {
                Exam2 = null;
            }
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            int TeacherID = Convert.ToInt32(Session["ID"]);
            int LessonID = Convert.ToInt32(Session["LessonID"]);
            sl = db.StudentLessons.Where(x => x.StudentNumber == StudentID && x.TeacherID == TeacherID && x.LessonID == LessonID).FirstOrDefault();
            if (string.IsNullOrEmpty(Exam1.ToString()))
            {

            }
            else if (Exam1 >= 0 || Exam1 <= 100)
            {
                sl.Exam1 = Exam1;
            }
            if (string.IsNullOrEmpty(Exam2.ToString()))
            {

            }
            else if (Exam2 >= 0 || Exam2 <= 100)
            {
                sl.Exam2 = Exam2;
            }

            db.SaveChanges();
            return RedirectToAction("ExamNote");
        }

        [HttpPost]
        public JsonResult BringStudents(int lesson_id)
        {
            Session["LessonID"] = lesson_id.ToString();
            List<StudentLesson> students = StudentL(lesson_id);
            return Json(students);
        }

        public List<StudentLesson> StudentL(int LessonID)
        {
            using (StudentAutomationContext db = new StudentAutomationContext())
            {
                int ID = Convert.ToInt32(Session["ID"]);
                return db.StudentLessons.Where(x => x.LessonID == LessonID && x.TeacherID == ID).ToList();
            }
        }

        [HttpPost]
        public JsonResult BringStudentName(int ID)
        {
            Session["StudentID"] = ID.ToString();
            List<Student> student = StudentList(ID);
            return Json(student);
        }

        public List<Student> StudentList(int StudentID)
        {
            using (StudentAutomationContext db = new StudentAutomationContext())
            {
                int ID = Convert.ToInt32(Session["ID"]);
                return db.Students.Where(x => x.StudentNumber == StudentID).ToList();
            }
        }

        [HttpPost]
        public JsonResult BringExamPoint(int ID)
        {
            List<StudentLesson> student = StudentLes(ID);
            return Json(student);
        }

        public List<StudentLesson> StudentLes(int StudentID)
        {
            using (StudentAutomationContext db = new StudentAutomationContext())
            {
                int ID = Convert.ToInt32(Session["ID"]);
                int LessonID = Convert.ToInt32(Session["LessonID"]);
                return db.StudentLessons.Where(x => x.StudentNumber == StudentID && x.LessonID == LessonID).ToList();
            }
        }

        public ActionResult StudentConfirm()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);

            StudentConfirmViewModel studentConfirm = new StudentConfirmViewModel();
            studentConfirm.Student = db.Students.Where(x => x.DepartmentID == DepartmentID).ToList();
            var list = db.StudentConfirmations.Where(x => x.TeacherID == ID).ToList();
            foreach (var item in list)
            {
                if (list.Where(x => x.Row == 1).FirstOrDefault() != null)
                {
                    studentConfirm.StudentConfirmation = db.StudentConfirmations.Where(x => x.TeacherID == ID).ToList();
                }
                else
                {
                    if (db.StudentConfirmations.Where(x => x.Row == item.Row - 1).FirstOrDefault().Confirmation ?? false)
                    {
                        studentConfirm.StudentConfirmation = db.StudentConfirmations.Where(x => x.TeacherID == ID).ToList();
                    }
                    else
                    {

                    }
                }

            }
            studentConfirm.Teacher = db.Teachers.Where(x => x.TeacherID == ID).ToList();
            studentConfirm.StudentRegistration = db.StudentRegistrations.Where(x => x.Status == false && x.DepartmentID == DepartmentID).ToList();

            return View(studentConfirm);
        }

        [HttpPost]
        public JsonResult Confirm(int ID)
        {
            int TeacherID = Convert.ToInt32(Session["ID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);
            StudentConfirmation sc = db.StudentConfirmations.FirstOrDefault(x => x.StudentRegisterNo == ID && x.TeacherID == TeacherID);
            StudentRegistration sr = db.StudentRegistrations.FirstOrDefault(x => x.StudentRegisterNo == ID);
            var list = db.StudentConfirmations.Where(x => x.TeacherID == TeacherID).ToList();


            foreach (var item in list)
            {
                if (db.StudentConfirmations.Where(x => x.Row == item.Row + 1).FirstOrDefault() == null)
                {
                    //Student student = new Student();

                    var model = Mapper.Map<StudentRegistration, Student>(sr); //global.asax'da ayarlanıyor.

                    model.RegistrationDate = DateTime.Now;
                    model.TeacherID = TeacherID;
                    //student.BirthDate = sr.BirthDate;
                    //student.BirthPlace = sr.BirthPlace;
                    //student.DepartmentID = sr.DepartmentID;
                    //student.Email = sr.Email;
                    //student.Image = sr.Image;
                    //student.Name = sr.Name;
                    //student.Password = sr.Password;
                    //student.RegistrationDate = DateTime.Now;
                    //student.Surname = sr.Surname;
                    //student.TeacherID = TeacherID;
                    //student.TrIdNo = sr.TrIdNo;


                    Mail mail = new Mail();

                    string body = string.Format("Sevgili Öğrenci Adayımız {0} {1}, Okulumuza {2} Tarihinde Yapmış Olduğunuz Başvurunuz Tarafımızca İncelendi ve Kabul Edildi. Okulumuza Gelmeniz Rica Olunur", sr.Name, sr.Surname, string.Format("{0:d}", sr.ApplicationDate));
                    mail.MailSender(body, sr.Email, "Öğrenci Başvurusu");

                    sr.Status = true;
                    sc.Confirmation = true;
                    db.Students.Add(model);
                    db.StudentConfirmations.RemoveRange(db.StudentConfirmations.Where(x => x.StudentRegisterNo == ID));
                    db.StudentRegistrations.Remove(sr);

                    db.SaveChanges();

                }
                else
                {
                    sc.Confirmation = true;
                    db.SaveChanges();
                }
            }

            return Json("");

        }

        [HttpPost]
        public JsonResult Refuse(int ID)
        {
            int TeacherID = Convert.ToInt32(Session["ID"]);
            int DepartmentID = Convert.ToInt32(Session["DepartmentID"]);

            StudentConfirmation sc = db.StudentConfirmations.FirstOrDefault(x => x.StudentRegisterNo == ID && x.TeacherID == TeacherID);
            StudentRegistration sr = db.StudentRegistrations.FirstOrDefault(x => x.StudentRegisterNo == ID);

            Mail mail = new Mail();

            string body = string.Format("Sevgili Öğrenci Adayımız {0} {1}, Okulumuza {2} Tarihinde Yapmış Olduğunuz Başvurunuz Tarafımızca İncelendi ve Reddedildi.", sr.Name, sr.Surname, string.Format("{0:d}", sr.ApplicationDate));
            mail.MailSender(body, sr.Email, "Öğrenci Başvurusu");

            sc.Confirmation = false;
            db.StudentConfirmations.RemoveRange(db.StudentConfirmations.Where(x => x.StudentRegisterNo == ID));
            db.StudentRegistrations.Remove(sr);

            db.SaveChanges();

            return Json("");
        }

        public ActionResult Settings()
        {
            ChangeLanguage();
            return View(List());
        }

        [HttpPost]
        public ActionResult Settings(string Name, string Surname, string BirthPlace, string Email, string Password)
        {
            int ID = Convert.ToInt32(Session["ID"]);
            Teacher t = db.Teachers.Where(x => x.TeacherID == ID).SingleOrDefault();

            if (Request.Files.Count > 0)
            {
                System.IO.File.Delete(Server.MapPath("~/TeacherImages/" + t.Image));

                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                string extension = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string filePath = "~/TeacherImages/" + fileName + extension;

                Request.Files[0].SaveAs(Server.MapPath(filePath));

                t.Image = fileName + extension;
            }

            t.Name = Name;
            t.Surname = Surname;
            t.BirthPlace = BirthPlace;
            t.Email = Email;
            t.Password = Password;

            db.SaveChanges();

            //TempData["Message"] = "Bilgileriniz Başarıyla Düzenlendi";
            return RedirectToAction("Index");
        }
        public void ChangeLanguage()
        {
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
        }

        public List<Teacher> List()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            var list = db.Teachers.Where(x => x.TeacherID == ID).ToList();

            return list;
        }
    }
}