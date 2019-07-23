using CodeFirstConsoleExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSendLayer;
namespace CodeFirstConsoleExample
{
    class Program
    {
        /* EF Table Auto Update İşlemi
         * enable-migrations
         * Configuration Dosyasına Bunları Ekle.
           AutomaticMigrationsEnabled = true;
           AutomaticMigrationDataLossAllowed = true;
           ContextKey = "CodeFirstConsoleExample.Models.OgrenciBilgiContext";
         * Context Dosyasına Bunları Ekle.(base kısmı)
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<OgrenciContext, Configuration>("OgrenciContext"));
         * Bundan Sonra Model Classlarında İstenilen Değişiklik Yapıldıktan Sonra Proje Debuglanınca Değişiklikler Otomatik Kaydedilir.
         */
        static void Main(string[] args)
        {
            OgrenciBilgiContext db = new OgrenciBilgiContext();
            Ogrenci o = new Ogrenci();

            foreach (var item in db.Ogrencis)
            {
                Console.WriteLine("Ad:" + item.Ad);
            }

            //db.Ogrencis.Add(new Ogrenci { Ad = "Engin", Yas = 27 });
            //db.SaveChanges();

            Console.WriteLine("\n");

            foreach (var item in db.Ogrencis)
            {
                Console.WriteLine("ad" + item.Ad);
                Console.WriteLine("yas" + item.Yas);
            }

            db.SaveChanges();
                                              /* Mail Gönderme */
            //Console.Write("Adınız: ");
            //string ad = Console.ReadLine();

            //Console.Write("Emailiniz: ");
            //string email = Console.ReadLine();

            //Console.Write("Email Şifreniz: ");
            //string sifre = Console.ReadLine();

            //Console.Write("Mesaj Atacağınız Mail Adresi: ");
            //string toMail = Console.ReadLine();

            //Console.Write("Başlık: ");
            //string baslik = Console.ReadLine();

            //Console.Write("Konu: ");
            //string konu = Console.ReadLine();

            //Console.Write("Mesaj: ");
            //string mesaj = Console.ReadLine();
             
            //Contact contact = new Contact();
            //Mail mail = new Mail();

            //var body = new StringBuilder();
            //body.AppendLine("Ad & Soyad: " + ad); //burada string ifadeler yerine inputlardan gelecek verilere aktarılan contact classındaki propertyler kullanılacak.
            //body.AppendLine("E-Mail Adresi: " + email);
            //body.AppendLine("Konu: " + konu);
            //body.AppendLine("Mesaj: " + mesaj);

            //mail.MailSender(body.ToString(), email, sifre, toMail, baslik);

            //Console.WriteLine("\n Mailiniz Başarıyla Gönderildi");

            Console.Read();
        }
    }
}
