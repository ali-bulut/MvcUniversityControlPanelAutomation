using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            // Primary Key
            this.HasKey(t => t.TeacherID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Surname)
                .HasMaxLength(50);

            this.Property(t => t.BirthPlace)
                .HasMaxLength(50);

            this.Property(t => t.TrIdNo)
                .HasMaxLength(11);

            this.Property(t => t.Image)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Teacher");
            this.Property(t => t.TeacherID).HasColumnName("TeacherID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.BirthPlace).HasColumnName("BirthPlace");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.TrIdNo).HasColumnName("TrIdNo");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.DepartmentID);

        }
    }
}
