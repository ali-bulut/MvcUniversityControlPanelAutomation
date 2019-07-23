using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentAutomationContext db = new StudentAutomationContext();
            
            db.SaveChanges();
            Console.WriteLine("Database Güncellendi");
            Console.ReadKey();
        }
    }
}
