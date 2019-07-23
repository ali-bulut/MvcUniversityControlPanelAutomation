using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.ViewModel
{
    public class StudentConfirmViewModel
    {
        public IEnumerable<StudentRegistration> StudentRegistration { get; set; }
        public IEnumerable<StudentConfirmation> StudentConfirmation { get; set; }
        public IEnumerable<Student> Student { get; set; }
        public IEnumerable<Teacher> Teacher { get; set; }

    }
}
