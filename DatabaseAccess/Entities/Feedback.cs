using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class Feedback
    {
        public string Id { get; set; }
        public int FeedbackUserId { get; set; }
        public int Type { get; set; }
        public string FeedbackContent { get; set; }
        public DateTime FeedbackDate { get; set; }
        public int? ReplyUserId { get; set; }
        public string ReplyContent { get; set; }
        public DateTime? ReplyDate { get; set; }
        public string ContestEventId { get; set; }
        public string ExchangeId { get; set; }
        public string ExchangeResponseId { get; set; }

        public virtual ContestEvent ContestEvent { get; set; }
        public virtual Exchange Exchange { get; set; }
        public virtual ExchangeResponse ExchangeResponse { get; set; }
        public virtual User FeedbackUser { get; set; }
        public virtual User ReplyUser { get; set; }
    }
}
