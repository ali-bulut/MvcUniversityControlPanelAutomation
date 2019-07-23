using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class TeacherLessonMap : EntityTypeConfiguration<TeacherLesson>
    {
        public TeacherLessonMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TeacherID, t.LessonID });

            // Properties
            this.Property(t => t.TeacherID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LessonID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Blank)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TeacherLesson");
            this.Property(t => t.TeacherID).HasColumnName("TeacherID");
            this.Property(t => t.LessonID).HasColumnName("LessonID");
            this.Property(t => t.Blank).HasColumnName("Blank");

            // Relationships
            this.HasRequired(t => t.Lesson)
                .WithMany(t => t.TeacherLessons)
                .HasForeignKey(d => d.LessonID);
            this.HasRequired(t => t.Teacher)
                .WithMany(t => t.TeacherLessons)
                .HasForeignKey(d => d.TeacherID);

        }
    }
}
