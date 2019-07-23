using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityLayer.Models.Mapping
{
    public class PasswordResetMap : EntityTypeConfiguration<PasswordReset>
    {
        public PasswordResetMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PasswordReset");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Guid).HasColumnName("Guid");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Authority).HasColumnName("Authority");

            // Relationships
            this.HasOptional(t => t.Student)
                .WithMany(t => t.PasswordResets)
                .HasForeignKey(d => d.UserID);

        }
    }
}
