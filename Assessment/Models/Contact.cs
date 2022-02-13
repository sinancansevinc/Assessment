using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]

        public string ContactContent { get; set; }
        [Required]

        public int PersonId { get; set; }
    }
}
