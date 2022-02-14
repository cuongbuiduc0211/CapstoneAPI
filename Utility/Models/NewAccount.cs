using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class NewAccount
    {
        [Required(ErrorMessage = "Username must be filled")]
        [MinLength(6, ErrorMessage = "Username from 6 to 50 characters")]
        [MaxLength(50, ErrorMessage = "Username from 6 to 50 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password must be filled")]
        [MinLength(6, ErrorMessage = "Password from 6 to 50 characters")]
        [MaxLength(50, ErrorMessage = "Password from 6 to 50 characters")]
        public string Password { get; set; }
        [MinLength(6, ErrorMessage = "FullName from 6 to 50 characters")]
        [MaxLength(200, ErrorMessage = "FullName from 6 to 200 characters")]
        public string FullName { get; set; }
        //[Range(1, 2, ErrorMessage = "RoleId must be 1 or 2")]
        public UserRole Role { get; set; }
    }
}
