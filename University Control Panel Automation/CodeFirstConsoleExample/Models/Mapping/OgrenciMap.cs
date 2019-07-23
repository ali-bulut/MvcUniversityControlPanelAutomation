using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeFirstConsoleExample.Models.Mapping
{
    public class OgrenciMap : EntityTypeConfiguration<Ogrenci>
    {
        public OgrenciMap()
        {
            // Primary Key
            this.HasKey(t => t.ogrenciID);

            // Properties
            this.Property(t => t.Ad)
                .HasMaxLength(50);

            this.Property(t => t.Soyad)
                .HasMaxLength(50);

            this.Property(t => t.No)
                .HasMaxLength(50);

            this.Property(t => t.Foto)
                .HasMaxLength(500);

            this.Property(t => t.Sehir)
                .HasMaxLength(50);

            this.Property(t => t.Ilce)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ogrenci");
            this.Property(t => t.ogrenciID).HasColumnName("ogrenciID");
            this.Property(t => t.Ad).HasColumnName("Ad");
            this.Property(t => t.Soyad).HasColumnName("Soyad");
            this.Property(t => t.No).HasColumnName("No");
            this.Property(t => t.Foto).HasColumnName("Foto");
            this.Property(t => t.Sehir).HasColumnName("Sehir");
            this.Property(t => t.Ilce).HasColumnName("Ilce");
        }
    }
}
