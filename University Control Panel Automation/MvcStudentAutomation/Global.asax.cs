using AutoMapper;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcStudentAutomation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(cfg => { 
                cfg.CreateMap<StudentRegistration, Student>().ReverseMap();
              //cfg.CreateMap<deneme, deneme>(); eğer ikinci map eklenecekse bu şekilde olmalı
            });
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
