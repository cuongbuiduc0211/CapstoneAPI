using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class ContestEvent
    {
        public ContestEvent()
        {
            ContestEventRegisters = new HashSet<ContestEventRegister>();
            ContestPrizes = new HashSet<ContestPrize>();
            Feedbacks = new HashSet<Feedback>();
        }

        public string Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public string Image { get; set; }
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartRegister { get; set; }
        public DateTime EndRegister { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Fee { get; set; }
        public int? CurrentParticipants { get; set; }
        public double? Rating { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string BrandId { get; set; }
        public string ProposalId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        public virtual Proposal Proposal { get; set; }
        public virtual ICollection<ContestEventRegister> ContestEventRegisters { get; set; }
        public virtual ICollection<ContestPrize> ContestPrizes { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
