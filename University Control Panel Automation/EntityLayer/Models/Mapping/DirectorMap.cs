using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class DirectorMap : EntityTypeConfiguration<Director>
    {
        public DirectorMap()
        {
            // Primary Key
            this.HasKey(t => t.DirectorID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Surname)
                .HasMaxLength(50);

            this.Property(t => t.TrIdNo)
                .HasMaxLength(11);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Director");
            this.Property(t => t.DirectorID).HasColumnName("DirectorID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.TrIdNo).HasColumnName("TrIdNo");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
        }
    }
}
