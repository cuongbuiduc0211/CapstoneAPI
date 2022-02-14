using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class ContestEventRegister
    {
        public string Id { get; set; }
        public string ContestEventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Status { get; set; }
        public double? Evaluation { get; set; }

        public virtual ContestEvent ContestEvent { get; set; }
        public virtual User User { get; set; }
    }
}
