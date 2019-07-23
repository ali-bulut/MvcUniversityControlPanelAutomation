using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            this.Students = new List<Student>();
            this.StudentConfirmations = new List<StudentConfirmation>();
            this.StudentLessons = new List<StudentLesson>();
            this.TeacherLessons = new List<TeacherLesson>();
        }

        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthPlace { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string TrIdNo { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<StudentConfirmation> StudentConfirmations { get; set; }
        public virtual ICollection<StudentLesson> StudentLessons { get; set; }
        public virtual ICollection<TeacherLesson> TeacherLessons { get; set; }
    }
}
