using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class StudentLesson
    {
        public int StudentNumber { get; set; }
        public int LessonID { get; set; }
        public int TeacherID { get; set; }
        public Nullable<int> Exam1 { get; set; }
        public Nullable<int> Exam2 { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
