using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class StudentConfirmationMap : EntityTypeConfiguration<StudentConfirmation>
    {
        public StudentConfirmationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.StudentRegisterNo, t.TeacherID });

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.StudentRegisterNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TeacherID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("StudentConfirmation");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.StudentRegisterNo).HasColumnName("StudentRegisterNo");
            this.Property(t => t.TeacherID).HasColumnName("TeacherID");
            this.Property(t => t.Row).HasColumnName("Row");
            this.Property(t => t.Confirmation).HasColumnName("Confirmation");

            // Relationships
            this.HasRequired(t => t.StudentRegistration)
                .WithMany(t => t.StudentConfirmations)
                .HasForeignKey(d => d.StudentRegisterNo);
            this.HasRequired(t => t.Teacher)
                .WithMany(t => t.StudentConfirmations)
                .HasForeignKey(d => d.TeacherID);

        }
    }
}
