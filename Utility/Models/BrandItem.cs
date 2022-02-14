using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class BrandItem
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be greater than 2 characters"),
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public BrandType Type { get; set; }
        
    }
}
