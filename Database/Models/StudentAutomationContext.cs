using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Database.Models.Mapping;

namespace Database.Models
{
    public partial class StudentAutomationContext : DbContext
    {
        static StudentAutomationContext()
        {
           System.Data.Entity.Database.SetInitializer<StudentAutomationContext>(null);
        }

        public StudentAutomationContext()
            : base("Name=StudentAutomationContext")
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentConfirmation> StudentConfirmations { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<StudentRegistration> StudentRegistrations { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherRegistration> TeacherRegistrations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DirectorMap());
            modelBuilder.Configurations.Add(new LessonMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new StudentConfirmationMap());
            modelBuilder.Configurations.Add(new StudentLessonMap());
            modelBuilder.Configurations.Add(new StudentRegistrationMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TeacherMap());
            modelBuilder.Configurations.Add(new TeacherRegistrationMap());
        }
    }
}
