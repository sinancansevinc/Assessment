using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Content { get; set; }
        public int EmployeeId { get; set; }
    }
}
