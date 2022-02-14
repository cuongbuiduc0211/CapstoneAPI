using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class ProposalItem
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public ProposalType Type { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        public string Image { get; set; }
        [RegularExpression("[0-9]+")]
        public int MinParticipants { get; set; }
        [RegularExpression("[0-9]+")]
        public int MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
