using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseAccess.Entities
{
    public partial class InterestedBrand
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string BrandId { get; set; }
        public int Status { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual User User { get; set; }
    }
}
