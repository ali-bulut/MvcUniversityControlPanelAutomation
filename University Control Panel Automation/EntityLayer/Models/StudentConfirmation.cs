using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class StudentConfirmation
    {
        public int ID { get; set; }
        public int StudentRegisterNo { get; set; }
        public int TeacherID { get; set; }
        public Nullable<int> Row { get; set; }
        public Nullable<bool> Confirmation { get; set; }
        public virtual StudentRegistration StudentRegistration { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
