using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class PasswordReset
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> Guid { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> Authority { get; set; }
        public virtual Student Student { get; set; }
    }
}
