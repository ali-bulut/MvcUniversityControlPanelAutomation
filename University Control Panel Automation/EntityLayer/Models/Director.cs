using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class Director
    {
        public int DirectorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TrIdNo { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
