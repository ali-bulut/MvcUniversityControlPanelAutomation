using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Database.Models.Mapping
{
    public class StudentLessonMap : EntityTypeConfiguration<StudentLesson>
    {
        public StudentLessonMap()
        {
            // Primary Key
            this.HasKey(t => new { t.StudentNumber, t.LessonID });

            // Properties
            this.Property(t => t.StudentNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LessonID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("StudentLesson");
            this.Property(t => t.StudentNumber).HasColumnName("StudentNumber");
            this.Property(t => t.LessonID).HasColumnName("LessonID");
            this.Property(t => t.Exam1).HasColumnName("Exam1");
            this.Property(t => t.Exam2).HasColumnName("Exam2");

            // Relationships
            this.HasRequired(t => t.Lesson)
                .WithMany(t => t.StudentLessons)
                .HasForeignKey(d => d.LessonID);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.StudentLessons)
                .HasForeignKey(d => d.StudentNumber);

        }
    }
}
