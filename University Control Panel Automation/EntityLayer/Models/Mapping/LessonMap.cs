using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class LessonMap : EntityTypeConfiguration<Lesson>
    {
        public LessonMap()
        {
            // Primary Key
            this.HasKey(t => t.LessonID);

            // Properties
            this.Property(t => t.LessonName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Lesson");
            this.Property(t => t.LessonID).HasColumnName("LessonID");
            this.Property(t => t.LessonName).HasColumnName("LessonName");
            this.Property(t => t.Credit).HasColumnName("Credit");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.Lessons)
                .HasForeignKey(d => d.DepartmentID);

        }
    }
}
