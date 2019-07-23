using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using EntityLayer.Models;
using MailSendLayer;
using System.Text;
using System.Net.Mail;
using System.Web.Helpers;
namespace MvcStudentAutomation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        StudentAutomationContext db = new StudentAutomationContext();
        public ActionResult SignIn()
        {
            if (Session["ID"] != null)
            {
                Session["ID"] = null;
            }
            if (Session["Email"] != null)
            {
                Session["Email"] = null;
            }
            if (Session["ResetID"] != null)
            {
                Session["ResetID"] = null;
            }
            //if (Request.Cookies["ID"] != null)
            //{
            //    var c = new HttpCookie("ID");
            //    c.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(c);
            //}
            var list = db.Teachers.ToList();
            var errorMessage = TempData["ErrorMessage"];
            ChangeLanguage();
            return View(list);
        }

        [HttpPost]
        public ActionResult SignIn(string Email, string Password)
        {
            string pass = Crypto.Hash(Password, "MD5");

            var isTrue = db.Teachers.Where(x => x.Email == Email && x.Password == pass).FirstOrDefault();

            if (isTrue != null)
            {
                Session["ID"] = isTrue.TeacherID.ToString();
                Session["DepartmentID"] = isTrue.DepartmentID.ToString();
                HttpCookie cookie = new HttpCookie("Name", isTrue.Name + " " + isTrue.Surname);
                Response.Cookies.Add(cookie);
                //HttpCookie cookie2 = new HttpCookie("ID", isTrue.TeacherID.ToString());
                //Response.Cookies.Add(cookie2);

                return RedirectToAction("Index", "Teacher");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı Email veya Şifre Girdiniz.";
                return RedirectToAction("SignIn");
            }
        }

        public ActionResult SignUp()
        {
            //if (Request.Cookies["Name"] != null)
            //{
            //    var c = new HttpCookie("Name");
            //    c.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(c);
            //}
            //if (Session["ID"] != null)
            //{
            //    Session["ID"] = null;
            //}
            var list = db.Departments.ToList();
            ChangeLanguage();
            return View(list);
        }
        [HttpPost]
        public JsonResult Register(string Name, string Surname, string BirthPlace, DateTime BirthDate, string TrIdNo, int DepartmentID, string Email, string Password)
        {
            TeacherRegistration t = new TeacherRegistration();
            var md5Pass = Password;
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string tamYolYeri = "~/TeacherImages/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));

                t.Image = dosyaAdi + uzanti;

                t.Name = Name;
                t.Surname = Surname;
                t.BirthPlace = BirthPlace;
                t.BirthDate = BirthDate;
                t.TrIdNo = TrIdNo;
                t.DepartmentID = DepartmentID;
                t.Email = Email;
                t.Password = Crypto.Hash(md5Pass, "MD5"); //cryptolayarak kaydetmek için.
                //t.Password = Password;
                t.ApplicationDate = DateTime.Now;
                var department = db.Departments.Where(x => x.DepartmentID == t.DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                db.TeacherRegistrations.Add(t);
                db.SaveChanges();

                Mail mail = new Mail();

                string body = string.Format("Sevgili {0} {1}, Okulumuzun {2} Bölümüne {3} Tarihinde Yapmış Olduğunuz Başvuru Tarafımıza İletilmiştir. En Yakın Zamanda Başvurunuz Cevaplanıp Size Mail Olarak İletilecektir.", t.Name, t.Surname, department, t.ApplicationDate);
                mail.MailSender(body, t.Email, "Öğretmen Başvurusu");
            }

            return Json("eklendi");
        }

        public ActionResult ForgetPassword()
        {
            List<Teacher> teacher = db.Teachers.ToList();
            return View(teacher);
        }

        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            try
            {  //authority => true ise student , false ise teacher
                Teacher teacher = db.Teachers.Where(x => x.Email == Email).FirstOrDefault();
                if (db.PasswordResets.Where(x => x.UserID == teacher.TeacherID && x.IsActive == true && x.Authority == false).SingleOrDefault() != null)
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
                reset.UserID = teacher.TeacherID;
                reset.Guid = guid;
                reset.CreationDate = DateTime.Now;
                reset.IsActive = true;
                reset.Authority = false; // teacher olduğunu söylemek için
                db.PasswordResets.Add(reset);
                db.SaveChanges();

                Mail mail = new Mail();

                string body = string.Format("Sevgili {0} {1}; Şifrenizi Unuttuğunuz Gerekçesiyle Bize Başvurdunuz. Şifrenizi Sıfırlamak İçin ", teacher.Name, teacher.Surname);
                body += "<a href='http://localhost:25835" + Url.Action("ResetPassword", "Login", new { @id = reset.Guid }) + "'>Tıklayın</a>";
                body += " Eğer Bu İstekten Haberiniz Yoksa Lütfen Şifrenizi Değiştirin.";
                mail.MailSender(body, teacher.Email, "Şifremi Unuttum");
                TempData["Message"] = "Şifre Sıfırlama Bağlantınız Mailinize Gonderilmistir.";
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
                var teacher = db.PasswordResets.SingleOrDefault(x => x.Guid == id && x.IsActive == true && x.Authority == false);

                Session["Email"] = teacher.Student.Email;
                Session["ResetID"] = teacher.ID.ToString();

                return View(teacher);
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
                var teacher = db.Teachers.Where(x => x.Email == email).SingleOrDefault();
                var reset = db.PasswordResets.Where(x => x.ID == resetID).SingleOrDefault();
                teacher.Password = Crypto.Hash(md5Pass, "MD5");
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
    }
}