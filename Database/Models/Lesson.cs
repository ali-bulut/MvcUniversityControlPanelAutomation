using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            this.StudentLessons = new List<StudentLesson>();
            this.Teachers = new List<Teacher>();
        }

        public int LessonID { get; set; }
        public string LessonName { get; set; }
        public Nullable<int> Credit { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<StudentLesson> StudentLessons { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
