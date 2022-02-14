using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class SelfProfile
    {
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(200, ErrorMessage = "Must be less than 200 characters")]
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime YearOfBirth { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DeviceToken { get; set; }        
    }
}
