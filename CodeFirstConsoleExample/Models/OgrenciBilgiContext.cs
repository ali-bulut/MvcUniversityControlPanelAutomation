using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CodeFirstConsoleExample.Models.Mapping;
using CodeFirstConsoleExample.Migrations;


namespace CodeFirstConsoleExample.Models
{
    public partial class OgrenciBilgiContext : DbContext
    {
        static OgrenciBilgiContext()
        {
            Database.SetInitializer<OgrenciBilgiContext>(null);
        }

        public OgrenciBilgiContext()
            : base("OgrenciBilgiContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OgrenciBilgiContext, Configuration>("OgrenciBilgiContext")); //eþzamanlý güncelleme için.
        }

        public DbSet<Ogrenci> Ogrencis { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OgrenciMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
