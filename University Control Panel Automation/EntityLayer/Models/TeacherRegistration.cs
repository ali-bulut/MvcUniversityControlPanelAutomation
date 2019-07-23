using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class TeacherRegistration
    {
        public int TeacherRegisterNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthPlace { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string TrIdNo { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public Nullable<bool> Confirmation { get; set; }
        public virtual Department Department { get; set; }
    }
}
