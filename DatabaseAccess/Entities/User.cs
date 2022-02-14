using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class User
    {
        public User()
        {
            ContestEventCreatedByNavigations = new HashSet<ContestEvent>();
            ContestEventModifiedByNavigations = new HashSet<ContestEvent>();
            ContestEventRegisters = new HashSet<ContestEventRegister>();
            ContestPrizeManagers = new HashSet<ContestPrize>();
            ContestPrizeUsers = new HashSet<ContestPrize>();
            ExchangeResponses = new HashSet<ExchangeResponse>();
            Exchanges = new HashSet<Exchange>();
            FeedbackFeedbackUsers = new HashSet<Feedback>();
            FeedbackReplyUsers = new HashSet<Feedback>();
            InterestedBrands = new HashSet<InterestedBrand>();
            PostCreatedByNavigations = new HashSet<Post>();
            PostModifiedByNavigations = new HashSet<Post>();
            ProposalManagers = new HashSet<Proposal>();
            ProposalUsers = new HashSet<Proposal>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string TokenWeb { get; set; }
        public string TokenMobile { get; set; }
        public string Image { get; set; }
        public int? Gender { get; set; }
        public DateTime? YearOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? Status { get; set; }
        public int? ExchangePost { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<ContestEvent> ContestEventCreatedByNavigations { get; set; }
        public virtual ICollection<ContestEvent> ContestEventModifiedByNavigations { get; set; }
        public virtual ICollection<ContestEventRegister> ContestEventRegisters { get; set; }
        public virtual ICollection<ContestPrize> ContestPrizeManagers { get; set; }
        public virtual ICollection<ContestPrize> ContestPrizeUsers { get; set; }
        public virtual ICollection<ExchangeResponse> ExchangeResponses { get; set; }
        public virtual ICollection<Exchange> Exchanges { get; set; }
        public virtual ICollection<Feedback> FeedbackFeedbackUsers { get; set; }
        public virtual ICollection<Feedback> FeedbackReplyUsers { get; set; }
        public virtual ICollection<InterestedBrand> InterestedBrands { get; set; }
        public virtual ICollection<Post> PostCreatedByNavigations { get; set; }
        public virtual ICollection<Post> PostModifiedByNavigations { get; set; }
        public virtual ICollection<Proposal> ProposalManagers { get; set; }
        public virtual ICollection<Proposal> ProposalUsers { get; set; }
    }
}
