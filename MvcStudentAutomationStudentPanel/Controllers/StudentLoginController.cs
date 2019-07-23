using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Models;
using MailSendLayer;
using System.Web.Helpers;
using System.Text;
namespace MvcStudentAutomationStudentPanel.Controllers
{
    public class StudentLoginController : Controller
    {
        StudentAutomationContext db = new StudentAutomationContext();
        // GET: Login
        public ActionResult SignIn()
        {
            if (Session["StudentID"] != null)
            {
                Session["StudentID"] = null;
            }
            if (Session["Email"] != null)
            {
                Session["Email"] = null;
            }
            if (Session["ResetID"] != null)
            {
                Session["ResetID"] = null;
            }
            var studentList = db.Students.ToList();
            var errorMessage = TempData["ErrorMessage"];
            return View(studentList);
        }

        [HttpPost]
        public ActionResult SignIn(string Email, string Password)
        {
            string pass = Crypto.Hash(Password, "MD5");
            var isTrue = db.Students.Where(x => x.Email == Email && x.Password == pass).FirstOrDefault();

            if (isTrue != null)
            {
                Session["StudentID"] = isTrue.StudentNumber.ToString();
                Session["DepartmentID"] = isTrue.DepartmentID.ToString();
                HttpCookie cookie = new HttpCookie("Name", isTrue.Name + " " + isTrue.Surname);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Student");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı Email veya Şifre Girdiniz.";
                return RedirectToAction("SignIn");
            }
        }
        public ActionResult SignUp()
        {
            var departmentList = db.Departments.ToList();
            return View(departmentList);
        }

        [HttpPost]
        public JsonResult Register(string Name, string Surname, string BirthPlace, DateTime BirthDate, string TrIdNo, int DepartmentID, string Email, string Password)
        {
            StudentRegistration t = new StudentRegistration();
            var md5Pass = Password;
            if (Request.Files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                string extension = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string file = "~/StudentImages/" + fileName + extension;
                Request.Files[0].SaveAs(Server.MapPath(file));

                t.Image = fileName + extension;

                t.Name = Name;
                t.Surname = Surname;
                t.BirthPlace = BirthPlace;
                t.BirthDate = BirthDate;
                t.TrIdNo = TrIdNo;
                t.DepartmentID = DepartmentID;
                t.Email = Email;
                // t.Password = Password;
                t.Password = Crypto.Hash(md5Pass, "MD5"); //cryptolayarak kaydetmek için.
                t.ApplicationDate = DateTime.Now;
                var department = db.Departments.Where(x => x.DepartmentID == t.DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                db.StudentRegistrations.Add(t);
                db.SaveChanges();

                Mail mail = new Mail();

                string body = string.Format("Sevgili {0} {1}, Okulumuzun {2} Bölümüne {3} Tarihinde Yapmış Olduğunuz Başvuru Tarafımıza İletilmiştir. En Yakın Zamanda Başvurunuz Cevaplanıp Size Mail Olarak İletilecektir.", t.Name, t.Surname, department, t.ApplicationDate);
                mail.MailSender(body, t.Email, "Öğrenci Başvurusu");

            }
            return Json("eklendi");
        }

        public ActionResult ForgetPassword()
        {
            List<Student> student = db.Students.ToList();
            return View(student);
        }

        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            try 
            { //authority => true ise student , false ise teacher
                Student student = db.Students.Where(x => x.Email == Email).SingleOrDefault();
                if (db.PasswordResets.Where(x => x.UserID == student.StudentNumber && x.IsActive == true && x.Authority == true).SingleOrDefault() != null)
                {
                    TempData["msg"] = "<script>alert('Zaten Daha Önce Böyle Bir Başvuru Yaptınız. Lütfen Mailinizi Kontrol Ediniz.');</script>";
                    return RedirectToAction("SignIn");
                }

                string guidKey = Guid.NewGuid().ToString();
                string lastGuid = null;

                foreach (char item in guidKey)
                {
                    if (char.IsNumber(item))
                    {
                        lastGuid += item; // harflerden ayrılmış değerler
                    }
                }

                lastGuid = lastGuid.Substring(0, 9);
                int guid = Convert.ToInt32(lastGuid);

                PasswordReset reset = new PasswordReset();
                reset.UserID = student.StudentNumber;
                reset.Guid = guid;
                reset.CreationDate = DateTime.Now;
                reset.Authority = true; // student olduğunu göstermek için
                reset.IsActive = true;

                db.PasswordResets.Add(reset);
                db.SaveChanges();
                Mail mail = new Mail();
                string body = string.Format("Sevgili {0} {1}; Şifrenizi Unuttuğunuz Gerekçesiyle Bize Başvurdunuz. Şifrenizi Sıfırlamak İçin ", student.Name, student.Surname);
                body += "<a href='http://localhost:27573" + Url.Action("ResetPassword", "StudentLogin", new { @id = reset.Guid }) + "'>Tıklayın</a>";
                body += " Eğer Bu İstekten Haberiniz Yoksa Lütfen Şifrenizi Değiştirin.";
                mail.MailSender(body, student.Email, "Şifremi Unuttum");
                TempData["Message"] = "Şifreniz Mailinize Gonderilmistir.";
                return RedirectToAction("SignIn");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Emailinizi Hatalı Girdiniz.";
                return RedirectToAction("ForgetPassword");
                throw ex;
            }
        }

        public ActionResult ResetPassword(int id)
        {
            try
            {
                var student = db.PasswordResets.SingleOrDefault(x => x.Guid == id && x.IsActive == true);

                Session["Email"] = student.Student.Email;
                Session["ResetID"] = student.ID.ToString();

                return View(student);
            }
            catch (Exception ex)
            {
                return RedirectToAction("SignIn");
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult ResetPassword(string Password, string PasswordAgain)
        {
            if (Password == PasswordAgain)
            {
                var md5Pass = Password;
                string email = Session["Email"].ToString();
                int resetID = Convert.ToInt32(Session["ResetID"]);
                var student = db.Students.Where(x => x.Email == email).SingleOrDefault();
                var reset = db.PasswordResets.Where(x => x.ID == resetID).SingleOrDefault();
                student.Password = Crypto.Hash(md5Pass, "MD5");
                reset.IsActive = false;
                db.SaveChanges();
                TempData["successMsg"] = "<script>alert('Şifreniz Başarıyla Değiştirildi');</script>";
                return RedirectToAction("SignIn");
            }
            else
            {
                TempData["errorMsg"] = "<script>alert('Şifreler Eşleşmiyor!');</script>";
                return RedirectToAction("ResetPassword");
            }

        }
    }
}