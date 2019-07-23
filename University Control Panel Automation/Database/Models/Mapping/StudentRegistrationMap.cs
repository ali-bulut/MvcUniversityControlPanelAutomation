using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Database.Models.Mapping
{
    public class StudentRegistrationMap : EntityTypeConfiguration<StudentRegistration>
    {
        public StudentRegistrationMap()
        {
            // Primary Key
            this.HasKey(t => t.StudentRegisterNo);

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

            this.Property(t => t.Password)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("StudentRegistration");
            this.Property(t => t.StudentRegisterNo).HasColumnName("StudentRegisterNo");
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
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.StudentRegistrations)
                .HasForeignKey(d => d.DepartmentID);

        }
    }
}
