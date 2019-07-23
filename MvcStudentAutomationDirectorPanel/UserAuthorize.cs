using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStudentAutomationDirectorPanel
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["DirectorID"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/DirectorLogin/SignIn");
                return false;
            }
        }
    }
}