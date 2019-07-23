using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Database.Models.Mapping
{
    public class StudentConfirmationMap : EntityTypeConfiguration<StudentConfirmation>
    {
        public StudentConfirmationMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("StudentConfirmation");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.StudentRegisterNo).HasColumnName("StudentRegisterNo");
            this.Property(t => t.TeacherID).HasColumnName("TeacherID");
            this.Property(t => t.Row).HasColumnName("Row");
            this.Property(t => t.Confirmation).HasColumnName("Confirmation");

            // Relationships
            this.HasOptional(t => t.StudentRegistration)
                .WithMany(t => t.StudentConfirmations)
                .HasForeignKey(d => d.StudentRegisterNo);
            this.HasOptional(t => t.Teacher)
                .WithMany(t => t.StudentConfirmations)
                .HasForeignKey(d => d.TeacherID);

        }
    }
}
