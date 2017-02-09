using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Data.Entity
{
    public class Group
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
        public bool SettledUp { get; set; }
    }
}
