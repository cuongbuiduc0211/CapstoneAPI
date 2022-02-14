using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Proposal
    {
        public Proposal()
        {
            ContestEvents = new HashSet<ContestEvent>();
        }

        public string Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public string Image { get; set; }
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ManagerId { get; set; }
        public string Reason { get; set; }

        public virtual User Manager { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ContestEvent> ContestEvents { get; set; }
    }
}
