using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class TeacherLesson
    {
        public int TeacherID { get; set; }
        public int LessonID { get; set; }
        public string Blank { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
