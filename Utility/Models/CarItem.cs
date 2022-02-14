using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class CarItem
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be greater than 2 characters"),
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string Name { get; set; }
        [Required]
        public string BrandId { get; set; }
        //[Required]
        //[RegularExpression("[0-9]{4}")]
        //public int Generation { get; set; }
        //[Required]
        //[RegularExpression("[0-9]+")]
        //public long Price { get; set; }
        //[Required]
        //public string Image { get; set; }
        //public DateTime? CreatedDate { get; set; }
        
    }
}
