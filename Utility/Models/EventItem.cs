using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models
{
    public class EventItem
    {
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [RegularExpression("[0-9]+")]
        public int MinParticipants { get; set; }
        [Required]
        [RegularExpression("[0-9]+")]
        public int MaxParticipants { get; set; }
        [Required]
        public DateTime StartRegister { get; set; }
        [Required]
        public DateTime EndRegister { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int CurrentParticipants { get; set; }
        public ContestEventStatus Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? ProposalId { get; set; }
    }
}
