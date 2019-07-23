using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStudentAutomation
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["ID"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/Login/SignIn");
                return false;
            }
        }
    }
}