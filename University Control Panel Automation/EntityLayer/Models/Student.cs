using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class Student
    {
        public Student()
        {
            this.PasswordResets = new List<PasswordReset>();
            this.StudentLessons = new List<StudentLesson>();
        }

        public int StudentNumber { get; set; }
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
        public Nullable<int> TeacherID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<PasswordReset> PasswordResets { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<StudentLesson> StudentLessons { get; set; }
    }
}