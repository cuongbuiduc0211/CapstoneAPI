using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class CERegister
    {
        [Required]
        public string ContestEventId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
