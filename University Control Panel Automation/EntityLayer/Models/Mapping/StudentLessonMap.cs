using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class StudentLessonMap : EntityTypeConfiguration<StudentLesson>
    {
        public StudentLessonMap()
        {
            // Primary Key
            this.HasKey(t => new { t.StudentNumber, t.LessonID, t.TeacherID });

            // Properties
            this.Property(t => t.StudentNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LessonID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TeacherID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("StudentLesson");
            this.Property(t => t.StudentNumber).HasColumnName("StudentNumber");
            this.Property(t => t.LessonID).HasColumnName("LessonID");
            this.Property(t => t.TeacherID).HasColumnName("TeacherID");
            this.Property(t => t.Exam1).HasColumnName("Exam1");
            this.Property(t => t.Exam2).HasColumnName("Exam2");

            // Relationships
            this.HasRequired(t => t.Lesson)
                .WithMany(t => t.StudentLessons)
                .HasForeignKey(d => d.LessonID);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.StudentLessons)
                .HasForeignKey(d => d.StudentNumber);
            this.HasRequired(t => t.Teacher)
                .WithMany(t => t.StudentLessons)
                .HasForeignKey(d => d.TeacherID);

        }
    }
}
