using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class PrizeItem
    {
        [Required]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters"),
         MaxLength(100, ErrorMessage = "Must be less than 100 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
