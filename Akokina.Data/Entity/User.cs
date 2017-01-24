using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Data.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
        public string Color { get; set; }
        public int Avatar { get; set; }
    }
}
