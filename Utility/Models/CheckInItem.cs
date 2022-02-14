using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class CheckInItem
    {
        [Required]
        public string ContestEventId { get; set; }
        [Required]
        public List<CheckIn> CheckIns { get; set; }
    }
    public class CheckIn
    {        
        [Required]
        public int UserId { get; set; }
        [Required]
        public UserEventContestStatus Status { get; set; }
    }
}



