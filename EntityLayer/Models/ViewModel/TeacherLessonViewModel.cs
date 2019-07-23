using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.ViewModel
{
    public class TeacherLessonViewModel
    {
        public IEnumerable<Teacher> Teacher { get; set; }
        public IEnumerable<TeacherLesson> TeacherLesson { get; set; }
        public IEnumerable<Lesson> Lesson { get; set; }
        public IEnumerable<Student> Student { get; set; }
        public IEnumerable<Director> Director { get; set; }
        public IEnumerable <StudentLesson> StudentLesson { get; set; }
        public IEnumerable <Department> Department { get; set; }
        public IEnumerable<StudentRegistration> StudentRegistration { get; set; }
        public IEnumerable<TeacherRegistration> TeacherRegistration { get; set; }
    }
}
