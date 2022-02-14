using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class ContestPrize
    {
        public string Id { get; set; }
        public string ContestId { get; set; }
        public string PrizeOrder { get; set; }
        public string PrizeId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ManagerId { get; set; }

        public virtual ContestEvent Contest { get; set; }
        public virtual User Manager { get; set; }
        public virtual Prize Prize { get; set; }
        public virtual User User { get; set; }
    }
}
