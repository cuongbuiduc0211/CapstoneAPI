using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class AccessoryItem
    {
        
        [Required]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters"), 
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string Name { get; set; }
        [Required]
        public string BrandId { get; set; }
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
