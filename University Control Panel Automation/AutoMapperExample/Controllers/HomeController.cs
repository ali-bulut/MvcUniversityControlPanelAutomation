using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapperExample.Models;
namespace AutoMapperExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Person person = new Person();
            person.FirstName = "Ali";
            person.LastName = "Bulut";
            AutoMapperr a = new AutoMapperr();
            var dest = a.Mapping(person);
            return View(dest);
        }
    }
}