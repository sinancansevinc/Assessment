using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.ViewModels
{
    public class ContactCreateDto
    {
        public int EmployeeId { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactContent { get; set; }
    }
}
