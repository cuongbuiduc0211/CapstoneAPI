using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class ExchangeCarItem
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
        public List<ExchangeCarDetails> ExchangeCarDetails { get; set; }

    }
    public class ExchangeCarDetails
    {
        [Required]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters"),
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string BrandId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters"),
         MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string CarName { get; set; }
        [Required]
        [RegularExpression("[0-9]{4}")]
        public int YearOfManufactor { get; set; }
        [MaxLength(50, ErrorMessage = "Must be less than 50 characters")]
        public string Origin { get; set; }
        [MaxLength(20, ErrorMessage = "Must be less than 20 characters")]
        public string LicensePlate { get; set; }
        public bool IsUsed { get; set; }
        [RegularExpression("[0-9]+\\.?[0-9]+")]
        public double Kilometers { get; set; }
        [RegularExpression("[0-9]+")]
        public int YearOfUsed { get; set; }
        public string Image { get; set; }
        [RegularExpression("[0-9]+")]
        public long Price { get; set; }
        [RegularExpression("[0-9]+")]
        public int Amount { get; set; }
    }
}
