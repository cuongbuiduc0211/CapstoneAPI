using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class AdminAccount
    {
        
        [Required(ErrorMessage = "Username must be filled")]
        [MinLength(6, ErrorMessage = "Username from 6 to 50 characters")]
        [MaxLength(50, ErrorMessage = "Username from 6 to 50 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password must be filled")]
        [MinLength(6, ErrorMessage = "Password from 6 to 50 characters")]
        [MaxLength(50, ErrorMessage = "Password from 6 to 50 characters")]
        public string Password { get; set; }
        public string DeviceToken { get; set; }


    }
}
