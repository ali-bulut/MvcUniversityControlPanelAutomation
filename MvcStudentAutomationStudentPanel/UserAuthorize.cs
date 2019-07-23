using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStudentAutomationStudentPanel
{
    //Authorization Filters -> Action methoddan önce çalışmaktadır. Controllere yada Action Metoda Erişimi Kısıtlamak İçin Kullanılır. AuthorizeAttribute 
    //Action Filters -> Action methoddan önce yada sonra çalışır. Action'a dışarıdan data göndermek, çalışmasını iptal etmek gibi görevler.
    //Result Filters -> ActionResult çalıştırılmadan önce yada sonra çalışır. OutputCacheAttribute
    //Exception Filters -> Hata durumlarında çalışan filtredir. Hata Mesajlarını Göstermek ve loglamak için kullanılabilir. HandleErrorAttribute 
    public class UserAuthorize : AuthorizeAttribute 
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["StudentID"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/StudentLogin/SignIn");
                return false;
            }
        }
    }
}