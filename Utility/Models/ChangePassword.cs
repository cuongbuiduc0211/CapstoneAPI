using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class ChangePassword
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password from 6 to 50 characters")]
        [MaxLength(50, ErrorMessage = "Password from 6 to 50 characters")]
        public string Password { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password from 6 to 50 characters")]
        [MaxLength(50, ErrorMessage = "Password from 6 to 50 characters")]
        public string ConfirmedPassword { get; set; }
    }
}
