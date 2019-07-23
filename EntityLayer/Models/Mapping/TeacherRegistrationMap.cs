using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class TeacherRegistrationMap : EntityTypeConfiguration<TeacherRegistration>
    {
        public TeacherRegistrationMap()
        {
            // Primary Key
            this.HasKey(t => t.TeacherRegisterNo);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Surname)
                .HasMaxLength(50);

            this.Property(t => t.BirthPlace)
                .HasMaxLength(50);

            this.Property(t => t.TrIdNo)
                .HasMaxLength(11);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TeacherRegistration");
            this.Property(t => t.TeacherRegisterNo).HasColumnName("TeacherRegisterNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.BirthPlace).HasColumnName("BirthPlace");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.TrIdNo).HasColumnName("TrIdNo");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.ApplicationDate).HasColumnName("ApplicationDate");
            this.Property(t => t.Confirmation).HasColumnName("Confirmation");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.TeacherRegistrations)
                .HasForeignKey(d => d.DepartmentID);

        }
    }
}
