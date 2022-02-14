using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class ExchangeResItem
    {
        [Required]
        public int UserId { get; set; }
        public string Message { get; set; }
        public string ExchangeId { get; set; }
    }
}
