using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Lessons = new List<Lesson>();
            this.Students = new List<Student>();
            this.StudentRegistrations = new List<StudentRegistration>();
            this.Teachers = new List<Teacher>();
            this.TeacherRegistrations = new List<TeacherRegistration>();
        }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<TeacherRegistration> TeacherRegistrations { get; set; }
    }
}
