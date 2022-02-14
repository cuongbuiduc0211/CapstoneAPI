using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Exchange
    {
        public Exchange()
        {
            ExchangeAccessorryDetails = new HashSet<ExchangeAccessorryDetail>();
            ExchangeCarDetails = new HashSet<ExchangeCarDetail>();
            ExchangeResponses = new HashSet<ExchangeResponse>();
            Feedbacks = new HashSet<Feedback>();
        }

        public string Id { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Total { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }

        public virtual City City { get; set; }
        public virtual District District { get; set; }
        public virtual User User { get; set; }
        public virtual Ward Ward { get; set; }
        public virtual ICollection<ExchangeAccessorryDetail> ExchangeAccessorryDetails { get; set; }
        public virtual ICollection<ExchangeCarDetail> ExchangeCarDetails { get; set; }
        public virtual ICollection<ExchangeResponse> ExchangeResponses { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
