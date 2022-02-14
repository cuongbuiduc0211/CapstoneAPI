using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class ExchangeAccItem
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }

        [Required]
        public List<ExchangeAccessorryDetails> ExchangeAccessorryDetails { get; set; }
    }
    public class ExchangeAccessorryDetails
    {
        [Required]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters"),
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string BrandId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters"),
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string AccessoryName { get; set; }
        public bool IsUsed { get; set; }
        [Required]
        public string Image { get; set; }       
        [RegularExpression("[0-9]+")]
        public long Price { get; set; }
        [RegularExpression("[0-9]+")]
        public int Amount { get; set; }
    }
}
