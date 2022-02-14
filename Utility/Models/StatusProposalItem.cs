using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class StatusProposalItem
    {
        [Required]
        public string id { get; set; }
        [Required]
        public int ManagerId { get; set; }
        public string Reason { get; set; }
    }
}
