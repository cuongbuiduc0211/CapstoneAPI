using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class ExchangeResponse
    {
        public ExchangeResponse()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public string Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public string ExchangeId { get; set; }

        public virtual Exchange Exchange { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
